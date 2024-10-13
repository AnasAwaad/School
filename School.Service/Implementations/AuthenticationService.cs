using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School.Service.Implementations;
public class AuthenticationService : IAuthenticationService
{
    #region Fields
    #endregion

    #region Constructor
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

        var symatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afldkflksanfjn3424235@$@$@slkfsdfl"));

        var signingCredentials = new SigningCredentials(symatricKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
                issuer: "http://localhost:5127/",
                audience: "http://localhost:4200/",
                expires: DateTime.Now.AddHours(1),
                claims: userClaims,
                signingCredentials: signingCredentials
            );

        // generate token
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    #endregion

}
