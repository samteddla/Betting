using FluentValidation;

namespace SportBet.Application.Teams;

public class AddTeamValidator : AbstractValidator<AddTeamCommand>
{
    public AddTeamValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(10);
        RuleFor(x => x.ShortName).NotEmpty().MinimumLength(3);
    }
}