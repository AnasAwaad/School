using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Commands.Models;
public class EditUserCommand : IRequest<Response<string>>
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
