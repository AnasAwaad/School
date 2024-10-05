using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Base;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repository;
public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    #region Fields
    private readonly DbSet<Department> _departments;
    #endregion


    #region Constructor
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
        _departments = context.Set<Department>();
    }
    #endregion

    #region Handle Functions

    #endregion
}
