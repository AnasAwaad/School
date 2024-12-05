using MediatR;
using School.Core.Bases;
using School.Data.Helper;

namespace School.Core.Features.Authentication.Commands.Models;
public class SignInCommand : IRequest<Response<JwtAuthResponse>>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsPersistent { get; set; } // To remember user across sessions
}
