using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students;
public partial class StudentProfile
{
    public void GetStudentListMapping()
    {
        CreateMap<Student, GetStudentListResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameEn, src.NameAr)))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department!.GetLocalized(src.Department.DNameEn, src.Department.DNameAr)));
    }
}
