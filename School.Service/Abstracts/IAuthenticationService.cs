using School.Data.Entities.Identity;

namespace School.Service.Abstracts;
public interface IAuthenticationService
{
    string GetJWTToken(ApplicationUser user);
}
