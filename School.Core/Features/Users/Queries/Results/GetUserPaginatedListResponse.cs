﻿namespace School.Core.Features.Users.Queries.Results;
public class GetUserPaginatedListResponse
{
    public string Id { get; set; }
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
