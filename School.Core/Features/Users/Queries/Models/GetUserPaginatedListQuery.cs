using MediatR;
using School.Core.Features.Users.Queries.Results;
using School.Core.Wrappers;

namespace School.Core.Features.Users.Queries.Models;
public class GetUserPaginatedListQuery : IRequest<PaginatedResult<GetUserPaginatedListResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
