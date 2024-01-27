using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMatchResultsCommand(int MatchTypeId, int MatchSelectionId) : IRequest<ErrorOr<GetMatchResult>>;

public class GetMatchResultsCommandHandler : IRequestHandler<GetMatchResultsCommand, ErrorOr<GetMatchResult>>
{
    private readonly BetContext _context;

    public GetMatchResultsCommandHandler(BetContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<GetMatchResult>> Handle(GetMatchResultsCommand request, CancellationToken cancellationToken)
    {
        var betResults = await _context.BetResults
                                    .Where(b => b.MatchSelectionId == request.MatchSelectionId
                                           && b.MatchTypeId == request.MatchTypeId)
                                    .Select(b => new MatchResult(b.MatchId, b.Outcome))
                                    .ToListAsync(cancellationToken);

        if (betResults == null)
        {
            return Error.NotFound("THE_BET_NOT_FOUND", "The bet was not found");
        }


        return ErrorOr.ErrorOr.From(
            new GetMatchResult(request.MatchTypeId, request.MatchSelectionId, betResults));
    }
}
