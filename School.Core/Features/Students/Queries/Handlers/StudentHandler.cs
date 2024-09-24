using AutoMapper;
using MediatR;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Queries.Handlers;
public class StudentHandler : IRequestHandler<GetStudentListQuery, List<GetStudentListResponse>>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    #endregion


    #region Constructors
    public StudentHandler(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    #endregion


    #region Handle Functions
    public async Task<List<GetStudentListResponse>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var studentList= await _studentService.GetStudentListAsync();
        var studentListMapper=_mapper.Map<List<GetStudentListResponse>>(studentList);
        return studentListMapper;
    }
    #endregion




}
