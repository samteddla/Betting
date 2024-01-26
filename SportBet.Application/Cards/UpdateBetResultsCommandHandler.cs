
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Contracts.Cards;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record UpdateBetResultsCommand(int MatchTypeId,int MatchSelectionId, IEnumerable<UpdateBetResultRequest> UpdateBetResultRequests) :IRequest<ErrorOr<IEnumerable<UpdateBetResult>>>
{
}

public class UpdateBetResultsCommandHandler : IRequestHandler<UpdateBetResultsCommand, ErrorOr<IEnumerable<UpdateBetResult>>>
{
    private readonly BetContext _context;

    public UpdateBetResultsCommandHandler(BetContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<UpdateBetResult>>> Handle(UpdateBetResultsCommand request, CancellationToken cancellationToken)
    {
        var betResults = await _context.BetResults
                                    .Where(b => b.MatchSelectionId == request.MatchSelectionId && b.MatchTypeId == request.MatchTypeId)
                                    .ToListAsync(cancellationToken);

        if (betResults == null)
        {
            return Error.NotFound("THE_BET_NOT_FOUND", "The bet was not found");
        }

        foreach (var betResult in betResults)
        {
            var updateBetResultRequest = request.UpdateBetResultRequests.FirstOrDefault(x => x.MatchId == betResult.MatchId);
            if (updateBetResultRequest != null)
            {
                betResult.Outcome = updateBetResultRequest.OutcomeId;
                betResult.ResultDate = DateTime.UtcNow;
                _context.BetResults.Update(betResult);
            }
        }

        return await _context.SaveChangesAsync(cancellationToken)
            .ContinueWith(t =>
                t.Result == 0
                    ? Error.Failure("No rows affected")
                    : ErrorOr.ErrorOr.From(request.UpdateBetResultRequests.Select(x => new UpdateBetResult($"Bet result updated for match {x.MatchId} with outcome {x.OutcomeId}")))
            );
    }
}
