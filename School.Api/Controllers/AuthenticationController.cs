using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Authentication.Commands.Models;
using School.Data.AppMetaData;

namespace School.Api.Controllers;
[ApiController]
public class AuthenticationController : AppControllerBase
{
    private readonly IMediator _madiator;

    public AuthenticationController(IMediator madiator)
    {
        _madiator = madiator;
    }

    [HttpPost(Router.AuthenticationRouting.SignIn)]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
    {
        var res = await _madiator.Send(request);
        return NewResult(res);
    }
}
