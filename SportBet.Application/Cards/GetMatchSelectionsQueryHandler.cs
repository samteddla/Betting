using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMatchSelectionsQuery() : IRequest<ErrorOr<IEnumerable<GetActiveMatchs>>>
{ }
public class GetMatchSelectionsQueryHandler : IRequestHandler<GetMatchSelectionsQuery, ErrorOr<IEnumerable<GetActiveMatchs>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetMatchSelectionsQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<GetActiveMatchs>>> Handle(GetMatchSelectionsQuery request, CancellationToken cancellationToken)
    {
        var matchSelections = await _context.MatchSelections
            .ToListAsync(cancellationToken)
            .ContinueWith(t => t.Result
            .Select(ms => new GetActiveMatchs(
                MatchSelectionId: ms.MatchSelectionId,
                Name: ms.Name,
                Description: ms.Description,
                IsEnabled: ms.IsEnabled))
            ).ContinueWith(t => t.Result.Where(ms => ms.IsEnabled));

        if (matchSelections == null)
        {
            return Error.NotFound(code: "MATCH_SELECTIONS_NOT_FOUND", description: "Match selections not found");
        }

        return ErrorOr.ErrorOr.From(matchSelections);
    }
}