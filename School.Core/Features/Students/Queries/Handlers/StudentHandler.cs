using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Service.Abstracts;
using System.Linq.Expressions;

namespace School.Core.Features.Students.Queries.Handlers;
public class StudentHandler : ResponseHandler,
                                IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>,
                                IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
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
    public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var studentList = await _studentService.GetStudentListAsync();
        var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);
        return Success(studentListMapper);
    }

    public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentWithDepartmentAsync(request.Id);

        if (student is null)
            return NotFound<GetStudentByIdResponse>();

        var studentMapper = _mapper.Map<GetStudentByIdResponse>(student);
        return Success(studentMapper);
    }

    public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Student, GetStudentPaginatedListResponse>> expression = student => new GetStudentPaginatedListResponse(student);

        var query = _studentService.GetFilteredStudentsAsQurable(request.OrderBy, request.Search);

        var paginatedResult = await query
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedResult;
    }




    #endregion




}
