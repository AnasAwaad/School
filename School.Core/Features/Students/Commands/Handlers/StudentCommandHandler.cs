using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;
using School.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Features.Students.Commands.Handlers;
public class StudentCommandHandler : ResponseHandler,
                                    IRequestHandler<AddStudentCommand, Response<string>>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor
    public StudentCommandHandler(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    #endregion

    #region Handle Functions
    public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var studentMapped=_mapper.Map<Student>(request);
        var studentResult =await _studentService.AddStudentAsync(studentMapped);

        if (studentResult == "Exists")
            return UnprocessableEntity<string>("Student name is exists");
        if (studentResult == "Success")
            return Created("Added successfully");

        return BadRequest<string>();
    }
    #endregion

}
