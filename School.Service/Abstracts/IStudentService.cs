using School.Data.Entities;

namespace School.Service.Abstracts;
public interface IStudentService
{
    Task<List<Student>> GetStudentListAsync();
}
