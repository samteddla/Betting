using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBet.Application.Cards;
using SportBet.Application.Games;
using SportBet.Application.Selections;
using SportBet.Contracts.Cards;
using SportBet.Contracts.Games;
using SportBet.Contracts.Selection;

namespace SportBet.Api;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BetController : ApiController
{
    // get cards
    [HttpGet("get-cards")]
    public async Task<ActionResult<IEnumerable<MyBets>>> GetBets()
    {
        var bets = await Sender.Send(new GetBetsQuery());

        return bets.Match(
           base.Ok,
           Problem);
    }

    // get card
    [HttpGet("get-card/{id}")]
    public async Task<ActionResult<IEnumerable<MyBet>>> GetBet(int id)
    {
        var bet = await Sender.Send(new GetBetsQueryCard(id));

        return bet.Match(
           base.Ok,
           Problem);
    }

    //GetMyBetExtendeResultQuery(int BetCardId)
    [HttpGet("get-card-extended/{id}")]
    public async Task<ActionResult<MyBetExtende>> GetBetExtended(int id)
    {
        var bet = await Sender.Send(new GetMyBetExtendeResultQuery(id));

        return bet.Match(
           base.Ok,
           Problem);
    }
    // get active bets
    [HttpGet("get-active-bets")]
    public async Task<ActionResult<IEnumerable<GetActivBetsResponse>>> GetAviliableBets()
    {
        var bets = await Sender.Send(new GetActivBetsQuery());

        return bets.Match(
           base.Ok,
           Problem);
    }

    // create match selections
    [HttpPost("create-match-selection")]
    public async Task<ActionResult<CreateMatchSelectionsResponse>> CreateMatchSelection(CreateMatchSelectionsRequest request)
    {
        var result = await Sender.Send(new CreateMatchSelectionsCommand(
                                                Name: request.Name,
                                                Description: request.Description,
                                               ActiveUntil: request.ActiveUntil,
                                               Matches: request.Matches,
                                               MatchesTypes: request.MatchesTypes));

        return result.Match(
            base.Ok,
            Problem);
    }

    // bet on selection
    [HttpPost("bet-on")]
    public async Task<ActionResult<BetOnGameResponse>> BetOnSelection(BetOnGame request)
    {
        var result = await Sender.Send(new BetOnSelectionCommand(
            SelectionId: request.SelectionId,
            MatchTypeId: request.MatchTypeId,
            Matches: request.Matches,
            Amount: request.Amount));

        return result.Match(
            data => base.Ok(data),
            errors => Problem(errors));
    }

    // get bet result card-id
    [HttpGet("get-bet-result/{id}")]
    public async Task<ActionResult<BetResultResponse>> GetBetResult(int id)
    {
        var bet = await Sender.Send(new GetBetResultQuery(id));

        return bet.Match(
           base.Ok,
           Problem);
    }

    // get-match-selections
    [HttpGet("get-match-selections")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetActiveMatchs>>> GetMatchSelections()
    {
        var bets = await Sender.Send(new GetMatchSelectionsQuery());

        return bets.Match(
           base.Ok,
           Problem);
    }
    
    // get-match-selections/{id} -- shows all selections for a match
    [HttpGet("get-match-selections/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<GetActiveMatch>> GetMatchSelections(int id)
    {
        var bets = await Sender.Send(new GetMatchSelectionQuery(id));

        return bets.Match(
           base.Ok,
           Problem);
    }

    // get-match-types
    [HttpGet("get-match-types")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetMatchTypes>>> GetMatchTypes()
    {
        var bets = await Sender.Send(new GetMatchTypesQuery());

        return bets.Match(
           base.Ok,
           Problem);
    }

    // get-match-types/{id} -- shows all match types for a match
    [HttpGet("get-match-types/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<GetMatchTypes>> GetMatchTypes(int id)
    {
        var bets = await Sender.Send(new GetMatchTypeQuery(id));

        return bets.Match(
           base.Ok,
           Problem);
    }

    // get-outcomes
    [HttpGet("get-outcomes")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetOutcomes>>> GetOutcomes()
    {
        var bets = await Sender.Send(new GetOutcomesQuery());

        return bets.Match(
           base.Ok,
           Problem);
    }

    // update-bet-result/{matchtypeid}
    [HttpPut("update-bet-result/{matchtypeid}")]
    public async Task<ActionResult<UpdateBetResult>> UpdateBetResult(int matchtypeId, UpdateBetResultRequest request)
    {
        var result = await Sender.Send(new UpdateBetResultCommand(
            matchtypeId,
            request.MatchSelectionId,
            request.MatchId,
            request.OutcomeId));

        return result.Match(
            base.Ok,
            Problem);
    }
}