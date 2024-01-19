using System.ComponentModel;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetBetsQueryCard(int BetCardId) : IRequest<ErrorOr<IEnumerable<MyBet>>>;

public class GetBetsQueryCardHandler : IRequestHandler<GetBetsQueryCard, ErrorOr<IEnumerable<MyBet>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetBetsQueryCardHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<MyBet>>> Handle(GetBetsQueryCard request, CancellationToken cancellationToken)
    {
        var query = from b in _context.BetSelections
                    join m in _context.Matches on b.MatchId equals m.MatchId into bm
                    from m in bm.DefaultIfEmpty()
                    join t in _context.Teams on m.AwayTeamId equals t.TeamId
                    join t2 in _context.Teams on m.HomeTeamId equals t2.TeamId
                    join o in _context.Outcomes on b.Outcome equals o.OutcomeId
                    join c in _context.BetCards on b.BetCardId equals c.BetCardId
                    join p in _context.People on c.PersonId equals p.PersonId
                    join mt in _context.MatchTypes on c.MatchTypeId equals mt.MatchTypeId
                    join ms in _context.MatchSelections on c.MatchSelectionId equals ms.MatchSelectionId
                    where b.BetCardId == request.BetCardId && c.PersonId == _userContext.UserId
                    select new
                    {
                        m.MatchId,
                        b.BetCardId,
                        ms.MatchSelectionId,
                        ms.Name,
                        ms.Description,
                        AwayTeamName = t.TeamName,
                        HomeTeamName = t2.TeamName,
                        Away = t.ShortName,
                        Home = t2.ShortName,
                        OutcomeName = o.Name,
                        OutComeId = o.OutcomeId,
                        MatchTypeName = mt.Name,
                        CreatedAt = b.Match.MatchDate
                    };

        if (!query.Any())
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }

        var result = await query.ToListAsync(cancellationToken)
                    .ContinueWith(t => t.Result.Select(b => new MyBet(
                                                                    BetCardId: b.BetCardId,
                                                                    MatchId: b.MatchId,
                                                                    HomeTeam: b.HomeTeamName,
                                                                    AwayTeam: b.AwayTeamName,
                                                                    MatchType: b.MatchTypeName,
                                                                    OutcomeId: b.OutComeId,
                                                                    OutcomeName: b.OutcomeName,
                                                                    CreatedAt: b.CreatedAt,
                                                                    Home: b.Home,
                                                                    Away: b.Away,
                                                                    MatchSelectionId: b.MatchSelectionId,
                                                                    MatchSelectionName: b.Name,
                                                                    MatchSelectionDescription: b.Description
                                                                )));

        return ErrorOr.ErrorOr.From(result);

    }
}