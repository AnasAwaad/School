using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Commands.Validators;
public class ChangePasswordUserValidator : AbstractValidator<ChangePasswordUserCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;

    #endregion

    #region Constructor
    public ChangePasswordUserValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion

    #region Handle Functions

    private void ApplyValidationRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKeys.PassAndConfrimPassNotEqual]);

    }

    private void ApplyCustomeValidationRules()
    {

    }
    #endregion



}
