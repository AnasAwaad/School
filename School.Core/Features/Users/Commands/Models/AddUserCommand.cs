﻿using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Commands.Models;
public class AddUserCommand : IRequest<Response<string>>
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
