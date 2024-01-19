using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Teams;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Teams;

public record AddTeamCommand(string Name, string ShortName) : IRequest<ErrorOr<TeamResponse>>;
public class AddTeamHandler : IRequestHandler<AddTeamCommand, ErrorOr<TeamResponse>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public AddTeamHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<TeamResponse>> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            TeamName = request.Name,
            ShortName = request.ShortName,
        };

        var tesmFound = await _context.Teams
            .Where(t => t.ShortName == team.ShortName)
            .SingleOrDefaultAsync(cancellationToken); ;

        if (tesmFound != null)
        {
            return Error.Conflict(code: "TEAM_ALREADY_EXISTS", description: "Team already exists");
        }

        await _context.Teams.AddAsync(team, cancellationToken);
        var updated = await _context.SaveChangesAsync(cancellationToken);

        if (updated == 0)
        {
            return Error.Conflict(code: "TEAM_ALREADY_EXISTS", description: "Team already exists");
        }

        return ErrorOr.ErrorOr.From(new TeamResponse(team.TeamId, team.TeamName, team.ShortName));
    }
}