using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    #region Fields
    private readonly IStudentService _studentService;
    #endregion
    #region Constructors
    public EditStudentValidator(IStudentService studentService)
    {
        _studentService = studentService;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion
    #region Handle Functions
    public void ApplyValidationRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull().WithMessage("{PropertyName} must not be null")
            .MaximumLength(20).WithMessage("{PropertyName} must be in {MaxLength} chars");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull().WithMessage("{PropertyName} must not be null")
            .MaximumLength(20).WithMessage("{PropertyName} must be in {MaxLength} chars");

    }

    public void ApplyCustomeValidationRules()
    {
        RuleFor(x => x.Name)
            .Must((model, CancellationToken) => !_studentService.IsStudentNameExistAsync(model.Name, model.Id))
            .WithMessage("{PropertyName} is exists");

    }
    #endregion
}
