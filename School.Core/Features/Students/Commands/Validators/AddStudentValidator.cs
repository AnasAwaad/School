﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constructors
    public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
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
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength20]);

        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength20]);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLength20]);

    }

    public void ApplyCustomeValidationRules()
    {
        RuleFor(x => x.NameEn)
            .Must((key, CancellationToken) => !_studentService.IsStudentNameExistAsync(key.NameEn))
            .WithMessage(_localizer[SharedResourcesKeys.Exists]);

        RuleFor(x => x.NameAr)
            .Must((key, CancellationToken) => !_studentService.IsStudentNameExistAsync(key.NameAr))
            .WithMessage(_localizer[SharedResourcesKeys.Exists]);
    }
    #endregion
}
