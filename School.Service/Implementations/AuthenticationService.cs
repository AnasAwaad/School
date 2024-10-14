using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helper;
using School.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School.Service.Implementations;
public class AuthenticationService : IAuthenticationService
{
    #region Fields
    private readonly JwtSettings jwtSettings;


    #endregion

    #region Constructor
    public AuthenticationService(JwtSettings jwtSettings)
    {
        this.jwtSettings = jwtSettings;
    }
    #endregion

    #region Handle Functions
    public string GetJWTToken(ApplicationUser user)
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
                expires: DateTime.Now.AddHours(1),
                claims: userClaims,
                signingCredentials: signingCredentials
            );

        // generate token
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    #endregion

}
