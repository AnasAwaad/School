using MediatR;
using School.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Features.Authentication.Commands.Models;
public class SignInCommand:IRequest<Response<string>>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsPersistent { get; set; } // To remember user across sessions
}
