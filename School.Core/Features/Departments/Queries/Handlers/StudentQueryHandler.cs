using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Queries.Handlers;
internal class StudentQueryHandler : ResponseHandler,
                                    IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IDepartmentService _departmentService;
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public StudentQueryHandler(IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService, IMapper mapper, IStudentService studentService) : base(localizer)
    {
        _localizer = localizer;
        _departmentService = departmentService;
        _mapper = mapper;
        _studentService = studentService;
    }
    #endregion

    #region Handle Functions
    public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        //get department by id include students , subjects ,instructors
        var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
        //if not exists return not founc
        if (department is null)
            return NotFound<GetDepartmentByIdResponse>();
        //if exists make mapping between request and response
        var departmentMapper = _mapper.Map<GetDepartmentByIdResponse>(department);


        var studentsQurable = _studentService.GetStudentsByDepartmentIdAsQurable(request.Id);

        var paginatedStudents = await studentsQurable
            .Select(s => new StudentResponse(s.StudID, s.GetLocalized(s.NameEn, s.NameAr)))
            .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize, messages: new List<string> { _localizer[SharedResourcesKeys.Success] });

        departmentMapper.StudentList = paginatedStudents;

        // return response
        return Success(departmentMapper);
    }
    #endregion
}
