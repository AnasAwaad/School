using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementations;
public class StudentService : IStudentService
{
    #region Fields
    private readonly IStudentRepository _studentRepository;
    #endregion


    #region Constructor
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    #endregion


    #region Handle Functions
    public async Task<List<Student>> GetStudentListAsync()
    {
        return await _studentRepository.GetStudentListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetTableNoTracking()
            .Include(s => s.Department)
            .Where(s => s.StudID == id)
            .FirstOrDefaultAsync();
        return student;
    }

    public async Task<string> AddStudentAsync(Student student)
    {
        // check if the name is exists or not
        var isExistStudent = _studentRepository.GetTableNoTracking().Any(s => s.Name == student.Name);

        if (isExistStudent)
            return "Exists";

        // add new student
        await _studentRepository.AddAsync(student);
        return "Success";
    }
    #endregion
}
