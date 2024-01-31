using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetBetResultQuery(int CardId) : IRequest<ErrorOr<BetResultResponse>>
{ }
public class GetBetResultQueryHandler : IRequestHandler<GetBetResultQuery, ErrorOr<BetResultResponse>>
{
    private readonly BetContext _context;
    private readonly IUserContext _userContext;

    public GetBetResultQueryHandler(BetContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ErrorOr<BetResultResponse>> Handle(GetBetResultQuery request, CancellationToken cancellationToken)
    {
        var query = from b in _context.BetCards
                    join s in _context.BetSelections on b.BetCardId equals s.BetCardId
                    join r in _context.BetResults on new { s.MatchId, b.MatchSelectionId, b.BetMatchType } equals new { r.MatchId, r.MatchSelectionId, r.BetMatchType } into br
                    from r in br.DefaultIfEmpty()
                    where b.BetCardId == request.CardId
                    && b.PersonId == _userContext.UserId
                    select new
                    {
                        MatchResultId = r != null ? r.Outcome : 0,
                        BetSelectionOutcome = s.Outcome,
                        s.BetSelectionId,
                        s.MatchId,
                        b.BetCardId,
                        b.BetAmount,
                        b.WonAmount,
                        b.MatchTypeId,
                        b.MatchSelectionId,
                        ReultMatchTypeId = r == null ? 0 : r.MatchTypeId == b.MatchTypeId ? b.MatchTypeId : 0,
                        MatchResult = r == null || r.Outcome == 0 ? "NotPlayed" : r.Outcome == s.Outcome ? "Win" : "Loss",
                        WinCount = r == null || r.Outcome == 0 ? 0 : r.Outcome == s.Outcome ? 1 : 0
                    };

        var result = await query.ToListAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(b => new MatchResponse(
                                                                MatchId: b.MatchId,
                                                                OutcomeId: b.BetSelectionOutcome,
                                                                MatchResult: b.MatchResult,
                                                                MatchResultId: b.MatchResultId
                                                                )).ToList());
        if (result.Count == 0)
        {
            return Error.NotFound("THE_BET_NOT_FOUND", "The bet was not found");
        }

        List<MatchResponse> matchResponses = new();
        int[] originalOutcomes = { 1, 2, 4, 5, 3, 6, 7 };
        List<List<int>> combinedOutcomes = new()
        {
            new List<int> { 1 },
            new List<int> { 2 },
            new List<int> { 4 },
            new List<int> { 1, 4 },
            new List<int> { 1, 2 },
            new List<int> { 2, 4 },
            new List<int> { 1, 2, 4 }
        };

        var dictionary = originalOutcomes.Zip(combinedOutcomes, (k, v) => new { k, v })
                                 .ToDictionary(x => x.k, x => x.v);

        var bothOutcomes = new List<int> { 5, 3, 6, 7 };
        foreach (var item in result)
        {
            var matchResult = "NotPlayed";
            if (item.OutcomeId != 0)
            {
                if (item.OutcomeId == item.MatchResultId)
                {
                    matchResult = "Win";
                }
                else
                {
                    var r = new List<int>();
                    var o = new List<int>();
                    dictionary.TryGetValue(item.MatchResultId, out r);
                    dictionary.TryGetValue(item.OutcomeId, out o);

                    var matchResultIds = bothOutcomes.Contains(item.MatchResultId) ? r : new List<int> { item.MatchResultId };
                    var outcomeIds = bothOutcomes.Contains(item.OutcomeId) ? o : new List<int> { item.OutcomeId };

                    if (matchResultIds != null && matchResultIds.Any() && outcomeIds != null && outcomeIds.Any())
                    {
                        var win = matchResultIds.Intersect(outcomeIds).Count() == 1;
                        matchResult = win ? "Win" : "Loss";
                    }
                }
            }

            MatchResponse response = new(
                                            MatchId: item.MatchId,
                                            OutcomeId: item.OutcomeId,
                                            MatchResult: matchResult,
                                            MatchResultId: item.MatchResultId
                                        );
            matchResponses.Add(response);
        }

        var totalWinCount = matchResponses.Select(m => m.MatchResult).Where(m => m == "Win").Count();
        return ErrorOr.ErrorOr.From(new BetResultResponse(
                                                Matches: matchResponses,
                                                TotalWinCount: totalWinCount,
                                                BetAmount: query.Select(b => b.BetAmount).FirstOrDefault(),
                                                WonAmount: query.Select(b => b.WonAmount).FirstOrDefault(),
                                                CardId: request.CardId,
                                                MatchSelectionId: query.Select(b => b.MatchSelectionId).FirstOrDefault(),
                                                MatchTypeId: query.Select(b => b.MatchTypeId).FirstOrDefault()
                                                ));

    }
}