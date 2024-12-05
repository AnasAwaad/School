using MediatR;
using School.Core.Bases;
using School.Data.Helper;

namespace School.Core.Features.Authentication.Commands.Models;
public class RefreshTokenCommand : IRequest<Response<JwtAuthResponse>>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
