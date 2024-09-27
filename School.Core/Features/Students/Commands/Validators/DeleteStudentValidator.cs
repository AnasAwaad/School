using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
{
    #region
    private readonly IStudentService _studentService;
    #endregion


    #region Constructors
    public DeleteStudentValidator(IStudentService studentService)
    {
        _studentService = studentService;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion
    #region Handle Functions
    public void ApplyValidationRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull().WithMessage("{PropertyName} must not be null");
    }

    public void ApplyCustomeValidationRules()
    {

    }
    #endregion
}

