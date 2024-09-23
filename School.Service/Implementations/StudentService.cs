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
    #endregion
}
