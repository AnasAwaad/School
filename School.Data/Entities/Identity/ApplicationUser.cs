﻿using Microsoft.AspNetCore.Identity;

namespace School.Data.Entities.Identity;
public class ApplicationUser : IdentityUser
{
    public string Address { get; set; }
    public string Country { get; set; }
}
