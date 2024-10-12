using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Commands.Models;
public class DeleteUserCommand : IRequest<Response<string>>
{
    public string Id { get; set; } = null!;
    public DeleteUserCommand(string id)
    {
        Id = id;
    }
}
