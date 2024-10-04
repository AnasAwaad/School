using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Handlers;
public class StudentCommandHandler : ResponseHandler,
                                    IRequestHandler<AddStudentCommand, Response<string>>,
                                    IRequestHandler<EditStudentCommand, Response<string>>,
                                    IRequestHandler<DeleteStudentCommand, Response<string>>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion

    #region Constructor
    public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localizer = localizer;
    }
    #endregion

    #region Handle Functions
    public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var studentMapped = _mapper.Map<Student>(request);
        var studentResult = await _studentService.AddStudentAsync(studentMapped);

        if (studentResult == "Success")
            return Created("");

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
        student = _mapper.Map(request, student);
        // call service for edit student
        var studentResult = await _studentService.EditStudentAsync(student);
        // return response
        if (studentResult == "Success")
            return Success("", message: _localizer[SharedResourcesKeys.Updated]);
        return BadRequest<string>();
    }

    public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        // check if the student id is exists
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        // return not found if not exist
        if (student is null)
            return NotFound<string>();
        // call service to delete student
        var studentResult = await _studentService.DeleteStudentAsync(student);
        // return response
        if (studentResult == "Success")
            return Delete<string>();
        return BadRequest<string>();
    }
    #endregion

}
