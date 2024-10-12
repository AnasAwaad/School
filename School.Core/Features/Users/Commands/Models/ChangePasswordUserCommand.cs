using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Commands.Models;
public class ChangePasswordUserCommand : IRequest<Response<string>>
{
    public string Id { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
