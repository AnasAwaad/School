using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Queries.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Queries.Handlers;
public class AuthenticationQueryHandler : ResponseHandler,
                                        IRequestHandler<ValidateTokenQuery, Response<string>>
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationQueryHandler(IStringLocalizer<SharedResources> localizer, IAuthenticationService authenticationService) : base(localizer)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Response<string>> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
    {
        var res = await _authenticationService.ValidateTokenAsync(request.AccessToken);
        return Success(res);
    }
}
