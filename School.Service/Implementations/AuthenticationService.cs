using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helper;
using School.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Service.Implementations;
public class AuthenticationService : IAuthenticationService
{
    #region Fields
    private readonly JwtSettings jwtSettings;
    private readonly ConcurrentDictionary<string, RefreshTokenResponse> _userRefreshToken;

    #endregion

    #region Constructor
    public AuthenticationService(JwtSettings jwtSettings)
    {
        this.jwtSettings = jwtSettings;
        _userRefreshToken = new ConcurrentDictionary<string, RefreshTokenResponse>();
    }
    #endregion

    #region Handle Functions
    public JwtAuthResponse GetJWTToken(ApplicationUser user)
    {

        // design token

        var userClaims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), // token generated id
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Role,"") // add role here
        };

        var symatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var signingCredentials = new SigningCredentials(symatricKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                claims: userClaims,
                signingCredentials: signingCredentials
            );

        // generate refresh token
        var refreshToken = GetRefreshToken(user.UserName!);

        return new JwtAuthResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
            RefreshToken = refreshToken
        };
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

        _userRefreshToken.AddOrUpdate(refreshToken.RefreshToken, refreshToken, (s, t) => refreshToken);

        return refreshToken;
    }
    #endregion

}
