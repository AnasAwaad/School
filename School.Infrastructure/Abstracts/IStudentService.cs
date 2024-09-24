using School.Data.Entities;
using School.Infrastructure.Base;

namespace School.Infrastructure.Abstracts;
public interface IStudentRepository:IGenericRepository<Student>
{
    Task<List<Student>> GetStudentListAsync();
}
