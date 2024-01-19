using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;
public record GetBetsQuery() : IRequest<ErrorOr<IEnumerable<MyBets>>>;
public class GetBetsQueryHandler : IRequestHandler<GetBetsQuery, ErrorOr<IEnumerable<MyBets>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetBetsQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<MyBets>>> Handle(GetBetsQuery request, CancellationToken cancellationToken)
    {
        var bets = await _context.BetCards
            .Where(b => b.PersonId == _userContext.UserId)
            .Include(b => b.BetMatchType)
            .Include(bs => bs.MatchSelection)
            .ToListAsync(cancellationToken);

        if (!bets.Any())
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }

        return ErrorOr.ErrorOr.From(bets.Select(b => new MyBets(
            BetCardId: b.BetCardId,
            MatchSelectionId: b.MatchSelectionId,
            MatchTypeId: b.MatchTypeId,
            MatchSelection: b.MatchSelection.Name,
            MatchType: b.BetMatchType.Name
        )));
    }
}