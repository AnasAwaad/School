namespace School.Core.Features.Students.Queries.Results;
public class GetStudentByIdResponse
{
    public int StudID { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? DepartmentName { get; set; }
}
