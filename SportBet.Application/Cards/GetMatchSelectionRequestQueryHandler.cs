using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMatchSelectionQuery(int MatchSelectionId) : IRequest<ErrorOr<GetActiveMatch>>;

public class GetMatchSelectionRequestQueryHandler : IRequestHandler<GetMatchSelectionQuery, ErrorOr<GetActiveMatch>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetMatchSelectionRequestQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<GetActiveMatch>> Handle(GetMatchSelectionQuery request, CancellationToken cancellationToken)
    {
        var query = from m in _context.MatchSelections
                    join ms in _context.MatchSelectionMatches on m.MatchSelectionId equals ms.SelectionId into mms
                    from ms in mms.DefaultIfEmpty()
                    join ma in _context.Matches on ms.MatchId equals ma.MatchId
                    join t in _context.Teams on ma.HomeTeamId equals t.TeamId
                    join t2 in _context.Teams on ma.AwayTeamId equals t2.TeamId
                    where m.MatchSelectionId == request.MatchSelectionId
                    select new
                    {
                        ms.MatchId,
                        HomeId = t.TeamId,
                        Home = t.TeamName,
                        HomeTeam = t.ShortName,
                        AwayId = t2.TeamId,
                        AwayTeam = t2.ShortName,
                        Away = t2.TeamName,
                        ma.MatchDate,
                        MatchSelections = m
                    };

        if (!query.Any())
        {
            return Error.NotFound(code: "MATCH_SELECTION_NOT_FOUND", description: "Match selection not found");
        }

        var matches = await query.ToListAsync(cancellationToken)
            .ContinueWith(t => t.Result.Select(ms => new SelectionMatchResponse(
                                                    MatchSelectionId: request.MatchSelectionId,
                                                    MatchId: ms.MatchId,
                                                    HomeId: ms.HomeId,
                                                    Home: ms.Home,
                                                    HomeTeam: ms.HomeTeam,
                                                    AwayId: ms.AwayId,
                                                    AwayTeam: ms.AwayTeam,
                                                    Away: ms.Away,
                                                    MatchDate: ms.MatchDate
                                                )));

        var matchSelections = await query.ToListAsync(cancellationToken)
            .ContinueWith(t => t.Result.Select(ms => ms.MatchSelections));

        var matchSelection = matchSelections.FirstOrDefault(ms => ms.MatchSelectionId == request.MatchSelectionId);

        if (matchSelection == null)
        {
            return Error.NotFound(code: "MATCH_SELECTION_NOT_FOUND2", description: "2Match selection not found");
        }

        var res = new GetActiveMatch(
            MatchSelectionId: matchSelection.MatchSelectionId,
            Name: matchSelection.Name,
            Description: matchSelection.Description,
            ActiveUntil: matchSelection.ActiveUntil,
            Matches: matches.ToList());

        return ErrorOr.ErrorOr.From(res);
    }
}
