using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Authentication.Commands.Validators;
public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;

    #endregion

    #region Constructor
    public RefreshTokenValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;

        ApplyValidationRules();
        ApplyCustomeValidationRules();
    }


    #endregion

    #region Handle Functions

    private void ApplyValidationRules()
    {

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

    }

    private void ApplyCustomeValidationRules()
    {

    }
    #endregion



}

