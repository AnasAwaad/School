using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Students.Commands.Models;
public class EditStudentCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int? DepartmentId { get; set; }
}
