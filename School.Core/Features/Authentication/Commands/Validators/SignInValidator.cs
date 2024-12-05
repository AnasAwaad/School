using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Authentication.Commands.Validators;
public class SignInValidator:AbstractValidator<SignInCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;

    #endregion

    #region Constructor
    public SignInValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion

    #region Handle Functions

    private void ApplyValidationRules()
    {
        
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength20]);
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

    }

    private void ApplyCustomeValidationRules()
    {

    }
    #endregion



}
