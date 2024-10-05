using School.Core.Features.Departments.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Departments;
public partial class DepartmentProfile
{
    public void GetDepartmentByIdMapping()
    {
        CreateMap<Department, GetDepartmentByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(dest => dest.DID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(dest => dest.GetLocalized(dest.DNameEn, dest.DNameAr)))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(dest => dest.GetLocalized(dest.InstructorManage.NameEn, dest.InstructorManage.NameAr)))
            .ForMember(dest => dest.StudentList, opt => opt.MapFrom(dest => dest.Students))
            .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(dest => dest.DepartmentSubjects))
            .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(dest => dest.Instructors));



        CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(dest => dest.GetLocalized(dest.NameEn, dest.NameAr)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(dest => dest.StudID));

        CreateMap<DepartmetSubject, SubjectResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(dest => dest.GetLocalized(dest.Subject.SubjectNameEn, dest.Subject.SubjectNameAr)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(dest => dest.SubID));


        CreateMap<Instructor, InstructorResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(dest => dest.GetLocalized(dest.NameEn, dest.NameAr)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(dest => dest.InstId));

    }
}
