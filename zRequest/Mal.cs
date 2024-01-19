// start from Sql Server Management Studio
var sql = $@"select ms.Name,c.BetCardId, ms.MatchSelectionId,ms.IsEnabled,  t.MatchTypeId, t.Name,c.* from BetCard c
 inner join MatchSelection ms on ms.MatchSelectionId = c.MatchSelectionId
 inner join BetMatchType t on t.MatchTypeId = c.MatchTypeId
 where c.PersonId = 1";

 // get active bets (MatchSelection) for SportBet.Api/Controllers/BetController.cs:
[HttpGet("get-active-bets")]
public async Task<ActionResult<IEnumerable<GetActivBetsResponse>>> GetAviliableBets()
{
    var bets = await Sender.Send(new GetActivBetsQuery());

    return bets.Match(
        base.Ok,
        Problem);
}

// snippet for SportBet.Application/Games/GetActivBetsQueryHandler.cs:
public record GetActivBetsQuery() : IRequest<ErrorOr<IEnumerable<GetActivBetsResponse>>>;
public class GetMatchSelectionsQueryHandler : IRequestHandler<GetActivBetsQuery, ErrorOr<IEnumerable<GetActivBetsResponse>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetActivBetsQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public Task<ErrorOr<IEnumerable<GetActivBetsResponse>>> Handle(GetActivBetsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// DTO : snippet for SportBet.Contracts/Games/GetActivBetsResponse.cs:
public record GetActivBetsResponse(int MatchSelectionId, int MatchTypeId, string MatchSelection, string MatchType);
public record ActiveBetRequest(int MatchId);
