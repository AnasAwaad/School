using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Commands.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;

namespace School.Core.Features.Users.Commands.Handlers;
public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserCommandHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IMapper mapper) : base(localizer)
    {
        _localizer = localizer;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithEmail is not null)
            return BadRequest<string>(_localizer[SharedResourcesKeys.EmailIsExist]);

        var userWithUserName = await _userManager.FindByNameAsync(request.UserName);

        if (userWithUserName is not null)
            return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);

        var user = _mapper.Map<ApplicationUser>(request);

        var res = await _userManager.CreateAsync(user, request.Password);
        if (!res.Succeeded)
            return BadRequest<string>(_localizer[SharedResourcesKeys.FaildToAddUser]);

        return Success("");
    }
}
