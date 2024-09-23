using School.Data.Entities;

namespace School.Infrastructure.Abstracts;
public interface IStudentRepository
{
    Task<List<Student>> GetStudentListAsync();
}
