using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Teams;
using SportBet.Infrastructure;

namespace SportBet.Application.Teams;

public class GetTeamsQuery : IRequest<ErrorOr<IEnumerable<TeamResponse>>> { }
public class GetTeamsHandler : IRequestHandler<GetTeamsQuery, ErrorOr<IEnumerable<TeamResponse>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetTeamsHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<TeamResponse>>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _context.Teams.ToListAsync(cancellationToken);

        if (!teams.Any())
        {
            return Error.Failure("TEAM_NOT_FOUND", "Team not found");
        }

        var responses = teams.Select(team => new TeamResponse(team.TeamId, team.TeamName, team.ShortName));

        return ErrorOr.ErrorOr.From(responses);
    }
}
