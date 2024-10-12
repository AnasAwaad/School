using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Commands.Validators;
public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _localizer;

    #endregion

    #region Constructor
    public DeleteUserValidator(IStringLocalizer<SharedResources> localizer)
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
    }

    private void ApplyCustomeValidationRules()
    {

    }
    #endregion
}
