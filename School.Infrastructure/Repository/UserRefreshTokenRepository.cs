using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Base;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repository;
public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
{
    #region Fields
    private readonly DbSet<UserRefreshToken> _userRefreshTokens;
    #endregion


    #region Constructor
    public UserRefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
        _userRefreshTokens = context.Set<UserRefreshToken>();
    }
    #endregion

    #region Handle Functions

    #endregion
}
