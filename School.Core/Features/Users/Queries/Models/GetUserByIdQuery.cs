using MediatR;
using School.Core.Bases;
using School.Core.Features.Users.Queries.Results;

namespace School.Core.Features.Users.Queries.Models;
public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
{
    public string Id { get; set; }
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
}
