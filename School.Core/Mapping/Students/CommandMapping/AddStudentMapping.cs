﻿using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students;
public partial class StudentProfile
{
    public void AddStudentMapping()
    {
        CreateMap<AddStudentCommand, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
    }
}