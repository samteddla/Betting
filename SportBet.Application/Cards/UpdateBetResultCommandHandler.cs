
using System.Runtime.InteropServices;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record UpdateBetResultCommand(int MatchTypeId, int MatchSelectionId, int MatchId, int OutcomeId) : IRequest<ErrorOr<UpdateBetResult>>;
public class UpdateBetResultCommandHandler : IRequestHandler<UpdateBetResultCommand, ErrorOr<UpdateBetResult>>
{
    private readonly BetContext _context;

    public UpdateBetResultCommandHandler(BetContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<UpdateBetResult>> Handle(UpdateBetResultCommand request, CancellationToken cancellationToken)
    {
        var betResult = await _context.BetResults
                                    .Where(b => b.MatchId == request.MatchId
                                    && b.MatchSelectionId == request.MatchSelectionId
                                    && b.MatchTypeId == request.MatchTypeId)
                                .FirstOrDefaultAsync(cancellationToken);

        var result = await _context.Outcomes
                                    .Where(o => o.OutcomeId == request.OutcomeId)
                                    .FirstOrDefaultAsync(cancellationToken);
        if (betResult == null)
        {
            return Error.NotFound("THE_BET_NOT_FOUND", "The bet was not found");
        }

        betResult.Outcome = request.OutcomeId;
        betResult.ResultDate = DateTime.UtcNow;
        _context.BetResults.Update(betResult);
        return await _context.SaveChangesAsync(cancellationToken)
            .ContinueWith(t =>
                t.Result == 0
                    ? Error.Failure("No rows affected")
                    : ErrorOr.ErrorOr.From(new UpdateBetResult($"Match result updated for matchId {betResult.MatchTypeId} result {result!.Name}"))
            );
    }
}
