using School.Data.Entities.Identity;
using School.Data.Helper;

namespace School.Service.Abstracts;
public interface IAuthenticationService
{
    JwtAuthResponse GetJWTToken(ApplicationUser user);
}
