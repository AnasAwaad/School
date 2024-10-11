using School.Core.Features.Users.Queries.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Users;
public partial class UserProfile
{
    public void GetUserPaginatedListMapping()
    {
        CreateMap<ApplicationUser, GetUserPaginatedListResponse>();
    }
}
