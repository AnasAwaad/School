using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators;
public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constructors
    public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService)
    {
        _studentService = studentService;
        _localizer = localizer;
        _departmentService = departmentService;

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

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
    }

    public void ApplyCustomeValidationRules()
    {
        RuleFor(x => x.NameEn)
            .MustAsync(async (key, CancellationToken) => !await _studentService.IsStudentNameExistAsync(key))
            .WithMessage(_localizer[SharedResourcesKeys.Exists]);

        RuleFor(x => x.NameAr)
            .MustAsync(async (key, CancellationToken) => !await _studentService.IsStudentNameExistAsync(key))
            .WithMessage(_localizer[SharedResourcesKeys.Exists]);

        RuleFor(x => x.DepartmentId)
            .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key))
            .WithMessage(_localizer[SharedResourcesKeys.IsNotExists]);

        //RuleFor(x => x.DepartmentId)
        //   .Must((Key, CancellationToken) => false)
        //   .WithMessage(_localizer[SharedResourcesKeys.IsNotExists]);
    }
    #endregion
}
