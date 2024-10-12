using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Users.Commands.Models;
using School.Core.Features.Users.Queries.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers;

[ApiController]
public class UserController : AppControllerBase
{
    #region Fileds
    private readonly IMediator _mediator;


    #endregion


    #region Constructor
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion


    #region Handle Functions

    [HttpPost(Router.UserRouting.Create)]
    public async Task<IActionResult> AddUser(AddUserCommand request)
    {
        var response = await _mediator.Send(request);
        return NewResult(response);
    }


    [HttpGet(Router.UserRouting.List)]
    public async Task<IActionResult> GetUsersPaginatedList([FromQuery] GetUserPaginatedListQuery request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet(Router.UserRouting.GetById)]
    public async Task<IActionResult> GetUser(string id)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(id));
        return NewResult(response);
    }

    [HttpPut(Router.UserRouting.Edit)]
    public async Task<IActionResult> UpdateUser([FromBody] EditUserCommand request)
    {
        var response = await _mediator.Send(request);
        return NewResult(response);
    }

    [HttpDelete(Router.UserRouting.Delete)]
    public async Task<IActionResult> UpdateUser(string id)
    {
        var response = await _mediator.Send(new DeleteUserCommand(id));
        return NewResult(response);
    }

    [HttpPut(Router.UserRouting.ChangePassword)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserCommand request)
    {
        var response = await _mediator.Send(request);
        return NewResult(response);
    }

    #endregion
}
