﻿using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Queries.Handlers;
public class StudentHandler : ResponseHandler, IRequestHandler<GetStudentListQuery,Response<List<GetStudentListResponse>>>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    #endregion


    #region Constructors
    public StudentHandler(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    #endregion


    #region Handle Functions
    public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var studentList= await _studentService.GetStudentListAsync();
        var studentListMapper=_mapper.Map<List<GetStudentListResponse>>(studentList);
        return Success(studentListMapper);
    }
    #endregion




}
