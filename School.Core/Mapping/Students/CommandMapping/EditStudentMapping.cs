using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students;
public partial class StudentProfile
{
    public void EditStudentMapping()
    {
        CreateMap<EditStudentCommand, Student>()
            .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
    }
}
