using MediatR;
using School.Core.Features.Students.Queries.Models;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Queries.Handlers;
public class StudentHandler : IRequestHandler<GetStudentListQuery, List<Student>>
{
    #region Fields
    private readonly IStudentService _studentService;
    #endregion


    #region Constructors
    public StudentHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }
    #endregion


    #region Handle Functions
    public async Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        return await _studentService.GetStudentListAsync();
    }
    #endregion




}
