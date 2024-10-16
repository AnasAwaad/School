using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helper;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthenticationService : IAuthenticationService
{
    #region Fields
    private readonly JwtSettings jwtSettings;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion

    #region Constructor
    public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository, UserManager<ApplicationUser> userManager)
    {
        this.jwtSettings = jwtSettings;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _userManager = userManager;
    }
    #endregion

    #region Handle Functions
    public async Task<JwtAuthResponse> GetJWTTokenAsync(ApplicationUser user)
    {
        var (securityToken, accessToken) = GenerateAccessToken(user);

        // generate refresh token
        var refreshToken = GetRefreshToken(user.UserName!);

        var userRefreshToken = new UserRefreshToken
        {
            UserId = user.Id,
            Token = accessToken,
            RefreshToken = refreshToken.RefreshToken,
            JwtId = securityToken.Id,
            IsUsed = false,
            IsRevoked = false,
            AddedTime = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate)
        };

        await _userRefreshTokenRepository.AddAsync(userRefreshToken);

        return new JwtAuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<JwtAuthResponse> GetRefreshTokenAsync(string accessToken, string refreshToken)
    {
        // decode jwt token
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

        // validate access token
        if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            throw new SecurityTokenException("Algorithm is not valid");

        if (jwtToken.ValidTo > DateTime.UtcNow)
            throw new SecurityTokenException("token is not expired");

        // get user refresh token
        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

        var userRefreshToken = await _userRefreshTokenRepository.GetTableNoTracking()
                                                         .Include(r => r.User)
                                                         .FirstOrDefaultAsync(r => r.UserId == userId
                                                                    && r.RefreshToken == refreshToken
                                                                    && r.Token == accessToken);
        // validate refresh token
        if (userRefreshToken is null)
            throw new SecurityTokenException("refresh token is not found");

        if (userRefreshToken.ExpireDate < DateTime.UtcNow)
        {
            userRefreshToken.IsRevoked = true;
            userRefreshToken.IsUsed = false;
            await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);

            throw new SecurityTokenException("refresh token is expired");
        }

        if (userRefreshToken.User is null)
            throw new SecurityTokenException("user is not found");

        // generate accessToken
        var (securityToken, newAccesstoken) = GenerateAccessToken(userRefreshToken.User);

        userRefreshToken.Token = newAccesstoken;
        await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
        return new JwtAuthResponse
        {
            AccessToken = newAccesstoken,
            RefreshToken = new RefreshTokenResponse
            {
                ExpiredAt = userRefreshToken.ExpireDate,
                RefreshToken = userRefreshToken.RefreshToken!,
                UserName = userRefreshToken.User.UserName!
            }
        };
    }

    public async Task<string> ValidateTokenAsync(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { jwtSettings.Issuer },
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            ValidAudience = jwtSettings.Audience,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidateLifetime = jwtSettings.ValidateLifetime,
            ClockSkew = TimeSpan.Zero
        };

        var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
        try
        {
            if (validator is null)
                throw new SecurityTokenException("Invalid token");
            return "Not expired";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private (JwtSecurityToken, string) GenerateAccessToken(ApplicationUser user)
    {
        var symatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var signingCredentials = new SigningCredentials(symatricKey, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpireDate);
        var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpireDate),
                claims: GetUserClaims(user),
                signingCredentials: signingCredentials
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return (securityToken, accessToken);
    }

    private List<Claim> GetUserClaims(ApplicationUser user)
    {
        var userClaims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), // token generated id
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Role,"") // add role here
        };

        return userClaims;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }

    private RefreshTokenResponse GetRefreshToken(string userName)
    {
        var refreshToken = new RefreshTokenResponse
        {
            ExpiredAt = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate),
            UserName = userName,
            RefreshToken = GenerateRefreshToken()
        };

        return refreshToken;
    }
    #endregion
}
