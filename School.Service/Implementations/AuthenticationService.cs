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

namespace School.Service.Implementations;
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
            AddedTime = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate)
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

        if (jwtToken.ValidTo > DateTime.Now)
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


        if (userRefreshToken.ExpireDate < DateTime.Now)
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

    private (JwtSecurityToken, string) GenerateAccessToken(ApplicationUser user)
    {
        var symatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var signingCredentials = new SigningCredentials(symatricKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
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
        // Create a byte array to hold the random data
        var randomNumber = new byte[32];

        // Fill the array with random values
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        // Convert the byte array to a Base64 string for easy storage and transmission
        return Convert.ToBase64String(randomNumber);
    }

    private RefreshTokenResponse GetRefreshToken(string userName)
    {
        var refreshToken = new RefreshTokenResponse
        {
            ExpiredAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
            UserName = userName,
            RefreshToken = GenerateRefreshToken()
        };


        return refreshToken;
    }
    #endregion

}
