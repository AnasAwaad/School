﻿using MediatR;
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
    public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
    {
        var response = await _mediator.Send(query);
        return NewResult(response);
    }
    #endregion
}
