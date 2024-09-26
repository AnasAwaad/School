using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers;
[ApiController]
public class StudentController : AppControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Router.StudentRouting.List)]
    public async Task<IActionResult> GetStudentList()
    {
        var response = await _mediator.Send(new GetStudentListQuery());
        return NewResult(response);
    }

    [HttpGet(Router.StudentRouting.GetById)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var response = await _mediator.Send(new GetStudentByIdQuery(id));
        return NewResult(response);
    }

    [HttpPost(Router.StudentRouting.Create)]
    public async Task<IActionResult> CreateStudent(AddStudentCommand studentCommand)
    {
        var response = await _mediator.Send(studentCommand);
        return NewResult(response);
    }

    [HttpPut(Router.StudentRouting.Edit)]
    public async Task<IActionResult> EditStudent(EditStudentCommand studentCommand)
    {
        var response = await _mediator.Send(studentCommand);
        return NewResult(response);
    }
}
