using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students;
public partial class StudentProfile
{
    public void GetStudentListMapping()
    {
        CreateMap<Student, GetStudentListResponse>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department!.DName));
    }
}
