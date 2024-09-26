using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Handlers;
public class StudentCommandHandler : ResponseHandler,
                                    IRequestHandler<AddStudentCommand, Response<string>>,
                                    IRequestHandler<EditStudentCommand, Response<string>>
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
        var studentMapped = _mapper.Map<Student>(request);
        var studentResult = await _studentService.AddStudentAsync(studentMapped);

        if (studentResult == "Success")
            return Created("Added successfully");

        return BadRequest<string>();
    }

    public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        // check if the student id is exists or not
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        // if not exist return not found
        if (student is null)
            return NotFound<string>();
        // mapping between request and student
        var studentMapped = _mapper.Map<Student>(request);
        // call service for edit student
        var studentResult = await _studentService.EditStudentAsync(studentMapped);
        // return response
        if (studentResult == "Success")
            return Success($"Student with Id {studentMapped.StudID} Updated Successfully");
        return BadRequest<string>();
    }
    #endregion

}
