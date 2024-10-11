using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Users.Commands.Models;
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
    #endregion
}
