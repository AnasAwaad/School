using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Commands.Handlers;
public class AuthenticationHandler : ResponseHandler,
                                     IRequestHandler<SignInCommand, Response<string>>
{

    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthenticationService _authenticationService;
    #endregion


    #region Constructor
    public AuthenticationHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticationService authenticationService) : base(localizer)
    {
        _localizer = localizer;
        _userManager = userManager;
        _signInManager = signInManager;
        _authenticationService = authenticationService;
    }

    #endregion


    #region Handle Functions

    public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        // check for user and password

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
            return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsNotExist]);

        var found = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!found)
            return BadRequest<string>(_localizer[SharedResourcesKeys.InvalidLoginAttemp]);


        //generate token
        var token = _authenticationService.GetJWTToken(user);

        return Success(token);
    }

    #endregion


}
