using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion


    #region Constructors
    public DeleteStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
    {
        _studentService = studentService;
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion
    #region Handle Functions
    public void ApplyValidationRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
    }

    public void ApplyCustomeValidationRules()
    {

    }
    #endregion
}

