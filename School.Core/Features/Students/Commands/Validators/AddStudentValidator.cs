using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    #region Fields
    private readonly IStudentService _studentService;
    #endregion
    #region Constructors
    public AddStudentValidator(IStudentService studentService)
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
            .Must((key, CancellationToken) => !_studentService.IsStudentNameExistAsync(key.Name))
            .WithMessage("{PropertyName} is exists");

    }
    #endregion
}
