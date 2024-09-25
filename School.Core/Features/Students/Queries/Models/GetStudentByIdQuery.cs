﻿using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Results;

namespace School.Core.Features.Students.Queries.Models;
public class GetStudentByIdQuery:IRequest<Response<GetStudentByIdResponse>>
{
    public GetStudentByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}