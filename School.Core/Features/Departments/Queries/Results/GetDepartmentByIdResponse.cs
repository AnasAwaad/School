namespace School.Core.Features.Departments.Queries.Results;
public class GetDepartmentByIdResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ManagerName { get; set; }
    public IList<StudentResponse>? StudentList { get; set; }
    public IList<SubjectResponse>? SubjectList { get; set; }
    public IList<InstructorResponse>? InstructorList { get; set; }
}

public class StudentResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public class SubjectResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class InstructorResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
