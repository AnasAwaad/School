using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Queries.Models;
using School.Core.Features.Users.Queries.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities.Identity;

namespace School.Core.Features.Users.Queries.Handlers;
public class UserQueryHandler : ResponseHandler,
                              IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>,
                              IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
{
    #region Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public UserQueryHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IMapper mapper) : base(localizer)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    #endregion

    #region Handle Functions
    public async Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.AsQueryable();

        var pageinatedUser = await _mapper.ProjectTo<GetUserPaginatedListResponse>(users)
                                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return pageinatedUser;
    }

    public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
            return NotFound<GetUserByIdResponse>();

        var userMapped = _mapper.Map<GetUserByIdResponse>(user);

        return Success(userMapped);
    }

    #endregion

}
