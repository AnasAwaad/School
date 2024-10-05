using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Departments.Queries.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers;
[ApiController]
public class DepartmentController : AppControllerBase
{
    #region Fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Handle Functions
    [HttpGet(Router.DepartmentRouting.GetById)]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var response = await _mediator.Send(new GetDepartmentByIdQuery(id));
        return NewResult(response);
    }
    #endregion
}
