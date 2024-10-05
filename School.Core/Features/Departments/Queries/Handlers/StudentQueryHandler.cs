using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Results;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Queries.Handlers;
internal class StudentQueryHandler : ResponseHandler,
                                    IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public StudentQueryHandler(IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService, IMapper mapper) : base(localizer)
    {
        _localizer = localizer;
        _departmentService = departmentService;
        _mapper = mapper;
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

        // return response
        return Success(departmentMapper);
    }
    #endregion
}
