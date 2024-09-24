using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Base;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repository;
internal class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    #region Fields
    private readonly DbSet<Student> _students;
    #endregion

    #region Constructors
    public StudentRepository(ApplicationDbContext context): base(context)
    {
        _students = context.Set<Student>();
    }
    #endregion

    #region Handle Functions
    public async Task<List<Student>> GetStudentListAsync()
    {
        return await _students.Include(s=>s.Department).ToListAsync();
    }
    #endregion

}
