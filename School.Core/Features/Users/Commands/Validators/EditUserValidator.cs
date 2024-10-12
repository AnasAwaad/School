using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Commands.Validators;
public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;

    #endregion

    #region Constructor
    public EditUserValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion

    #region Handle Functions

    private void ApplyValidationRules()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength50]);

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength20]);

    }

    private void ApplyCustomeValidationRules()
    {

    }
    #endregion
}
