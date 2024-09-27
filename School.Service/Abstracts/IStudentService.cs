using School.Data.Entities;
using School.Data.Helper;

namespace School.Service.Abstracts;
public interface IStudentService
{
    Task<List<Student>> GetStudentListAsync();
    IQueryable<Student> GetStudentsAsQurable();
    IQueryable<Student> GetFilteredStudentsAsQurable(StudentOrdering? orderBy, string? search);
    Task<Student> GetStudentWithDepartmentAsync(int id);
    Task<Student> GetStudentByIdAsync(int id);
    Task<string> AddStudentAsync(Student student);
    Task<string> EditStudentAsync(Student student);
    Task<string> DeleteStudentAsync(Student student);
    bool IsStudentNameExistAsync(string name);
    bool IsStudentNameExistAsync(string name, int id);
}
