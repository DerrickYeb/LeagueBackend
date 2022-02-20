using FluentValidation;
using Shared.DTO.Team.Request.TeamRequests;

namespace Application.Validators.Team;

public class AddTeamRequestValidator:CustomValidator<TeamInputModel>
{
    public AddTeamRequestValidator()
    {
        RuleFor(p => p.ClubName).NotEmpty().MaximumLength(1000)
            .WithMessage("Club name cannot be empty and cannot exceed 1000 characters");
        RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address");
        RuleFor(p => p.Password).NotEmpty().MaximumLength(8).WithMessage("Password cannot be empty");
        RuleFor(p => p.Slogan).NotEmpty().WithMessage("Club slogan cannot be null");
        RuleFor(p => p.Website).NotEmpty().WithMessage("Club website cannot be empty");
        RuleFor(p => p.ShortHand).NotEmpty().WithMessage("Club short hand name cannot be empty");
        RuleFor(p => p.TeamCode).NotEmpty().WithMessage("Team code should not be empty");
        RuleFor(p => p.DivisionTypeId).NotEmpty().WithMessage("Club division type should be selected");
    }
}