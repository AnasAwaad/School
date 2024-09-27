using School.Data.Entities;

namespace School.Core.Features.Students.Queries.Results;
public class GetStudentPaginatedListResponse
{
    public GetStudentPaginatedListResponse(Student student)
    {
        Name = student.Name;
        StudentId = student.StudID;
        Address = student.Address;
        DepartmentName = student.Department!.DName;
    }
    public int StudentId { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? DepartmentName { get; set; }
}
