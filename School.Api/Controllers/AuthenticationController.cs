﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Api.Base;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Features.Authentication.Queries.Models;
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


    [HttpPost(Router.AuthenticationRouting.RefreshToken)]
    public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenCommand request)
    {
        var res = await _madiator.Send(request);
        return NewResult(res);
    }

    [HttpGet(Router.AuthenticationRouting.ValidateToken)]
    public async Task<IActionResult> ValidateToken([FromQuery] ValidateTokenQuery request)
    {
        var res = await _madiator.Send(request);
        return NewResult(res);
    }
}
