
code cs
```cs

GetMatchSelectionRequestQueryHandler.cs

var sql = $@"select * from MatchSelection m
                    inner join MatchSelectionMatch ms on m.MatchSelectionId = ms.SelectionId
                    inner join [Match] ma on ms.MatchId = ma.MatchId
                    inner join Team t on ma.HomeTeamId = t.TeamId
                    inner join Team t2 on ma.AwayTeamId = t2.TeamId
                    where m.MatchSelectionId = 4;";


var matchSelection = await _context.MatchSelections
            .Where(ms => ms.MatchSelectionId == request.MatchSelectionId)
            .Include(ms => ms.MatchSelectionMatches)
            .ThenInclude(m => m.Match)
            .ToListAsync(cancellationToken)
            .ContinueWith(t=> t.Result
            .Select(ms => new GetMatchSelectionResponse(ms.MatchSelectionId, ms.Name, ms.Description, ms.ActiveUntil, ms.MatchSelectionMatches
                .Select(m => new SelectionMatchResponse(
                    MatchSelectionId:m.SelectionId, 
                    MatchId : m.MatchId, 
                    HomeId : m.Match.HomeTeamId, 
                    Home : m.Match.HomeTeam.TeamName, 
                    HomeTeam : m.Match.HomeTeam.ShortName, 
                    AwayId : m.Match.AwayTeamId, 
                    AwayTeam : m.Match.AwayTeam.TeamName, 
                    Away : m.Match.AwayTeam.ShortName, 
                    MatchDate : m.Match.MatchDate)
                ).ToList())));

```

class GetBetResultQueryHandler

```cs
public async Task<ErrorOr<BetResultResponse>> Handle2(GetBetResultQuery request, CancellationToken cancellationToken)
    {
        /*
        var betCard = await _context.BetCards.FirstOrDefaultAsync(c => c.BetCardId == request.CardId 
        && c.PersonId == _userContext.UserId, cancellationToken);
        if (betCard == null)
        {
            return Error.NotFound($"Bet card with id {request.CardId} not found");
        }
        
        var results = await _context.BetResults
            .Where(r => r.MatchSelectionId == betCard.MatchSelectionId)
            .ToListAsync(cancellationToken);

        var betselections = await _context.BetSelections
            .Where(b => b.BetCardId == request.CardId)
            .ToListAsync(cancellationToken);
        
        foreach (var betselection in betselections)
        {
            var matchResult = results.FirstOrDefault(r => r.MatchId == betselection.MatchId);
            if (matchResult != null)
            {
                betselection.Outcome = matchResult.Outcome;
            }
            else
            {
                betselection.Outcome = 0;
            }
        }*/

        var query = from b in _context.BetSelections
                    join m in _context.Matches on b.MatchId equals m.MatchId
                    join o in _context.Outcomes on b.Outcome equals o.OutcomeId
                    join c in _context.BetCards on b.BetCardId equals c.BetCardId
                    join mt in _context.MatchTypes on c.MatchTypeId equals mt.MatchTypeId
                    join r in _context.BetResults on b.MatchId equals r.MatchId into br
                    from r in br.DefaultIfEmpty()
                    where b.BetCardId == request.CardId 
                    && c.PersonId == _userContext.UserId
                    && c.MatchSelectionId == r.MatchSelectionId
                    select new
                    {
                        MatchResultId = r != null ? r.Outcome : 0,
                        BetSelectionOutcome = b.Outcome,
                        MatchId = m.MatchId,
                        MatchTypeId = mt.MatchTypeId,
                        BetCardId = b.BetCardId,
                        OutcomeName = o.Name,
                        MatchTypeName = mt.Name,
                        BetAmount = c.BetAmount,
                        WonAmount = c.WonAmount,
                        MatchResult = r == null ? "NotPlayed" : r.Outcome == b.Outcome ? "Win" : "Loss"
                    };


        List<MatchResponse> result = await query.ToListAsync(cancellationToken)
                    .ContinueWith(t => t.Result.Select(b => new MatchResponse(
                                                                    MatchId: b.MatchId,
                                                                    OutcomeId: b.BetSelectionOutcome,
                                                                    MatchResult: b.MatchResult,
                                                                    MatchResultId: b.MatchResultId
                                                                    )).ToList());
        var ids = query.Select(r => r.MatchId).ToList();
        var betSelections = await _context.BetSelections
            .Include(b => b.BetCard)
            .Where(c => c.BetCardId == request.CardId)
            .ToListAsync(cancellationToken);

        if(betSelections.Count > 0)
        {
           foreach (var betCard in betSelections)
           {
                if(!ids.Contains(betCard.MatchId))
                {
                    result.Add(new MatchResponse (
                        MatchId: betCard.MatchId,
                        OutcomeId: betCard.Outcome,
                        MatchResult: "NotPlayed",
                        MatchResultId: 0
                    ));
                }
           }
        }
        var betAmount = query.Select(b => b.BetAmount).FirstOrDefault();
        var wonAmount = query.Select(b => b.WonAmount).FirstOrDefault();
        
        var ret = new BetResultResponse(
                                Matches: result, 
                                TotalWinCount:0, 
                                BetAmount: betAmount, 
                                WonAmount: wonAmount,
                                CardId : request.CardId);

        return ErrorOr.ErrorOr.From(ret);
    }

```

class : UpdateBetResultCommandHandler.cs

```cs
 public async Task<ErrorOr<UpdateBetResultResponse>> Handle2(UpdateBetResultCommand request, CancellationToken cancellationToken)
    {
        var sql = $@"UPDATE BetResults 
                        SET Outcome = {request.OutcomeId} 
                    WHERE 
                        MatchId = {request.MatchId} 
                    AND 
                        MatchSelectionId = {request.MatchSelectionId} 
                    AND 
                        MatchTypeId = {request.MatchTypeId}";
        var result = await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        if (result == 0)
        {
            return ErrorOr.ErrorOr.From(new UpdateBetResultResponse("No rows affected"));
        }
        return ErrorOr.ErrorOr.From(new UpdateBetResultResponse("Bet result updated"));
    }

```

class : CreateMatchSelectionsCommand.cs

```cs
var betMatchTypes = await _context.MatchTypes.ToListAsync(cancellationToken)
                    .ContinueWith(t => t.Result.Select(m => new BetMatchType
                    {
                        MatchTypeId = m.MatchTypeId,
                        Name = m.Name,
                        IsEnabled = true
                    }).Where(m => m.IsEnabled).ToList());

```