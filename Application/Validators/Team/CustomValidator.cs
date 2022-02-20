using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators.Team;

public class CustomValidator<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        var validationResult = base.Validate(context);
        if (validationResult.IsValid) return validationResult;
        var failures = validationResult.Errors.ToList();
        if (failures.Count != 0)
            throw new Exceptions.ValidationExceptions(failures.Select(s => s.ErrorMessage).ToList());

        return validationResult;
    }
}