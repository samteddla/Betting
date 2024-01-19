using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMyBetExtendeResultQuery(int BetCardId) : IRequest<ErrorOr<MyBetExtende>>;
public class GetMyBetExtendeResultQueryHandler : IRequestHandler<GetMyBetExtendeResultQuery, ErrorOr<MyBetExtende>>
{
    private readonly IUserContext _userContext;
    private readonly IMediator _mediator;
    private readonly BetContext _context;

    public GetMyBetExtendeResultQueryHandler(IUserContext userContext, IMediator mediator,BetContext betContext)
    {
        _userContext = userContext;
        _mediator = mediator;
        _context = betContext;
    }

    public async Task<ErrorOr<MyBetExtende>> Handle(GetMyBetExtendeResultQuery request, CancellationToken cancellationToken)
    {
        var myBets = await _mediator.Send(new GetBetsQueryCard(request.BetCardId));

        var myBestResults = await _mediator.Send(new GetBetResultQuery(request.BetCardId));

        if(myBestResults.IsError)
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }
        if(myBets.IsError)
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }

        var myBetMatchExtends = new List<MyBetMatchExtend>();
        var betResult = myBestResults.Value;
        foreach (var item in myBets.Value)
        {   
            myBetMatchExtends.Add(
            new MyBetMatchExtend(
                                    MatchId: item.MatchId,
                                    HomeTeam: item.HomeTeam,
                                    Home: item.Home,
                                    AwayTeam: item.AwayTeam,
                                    Away: item.Away,
                                    OutcomeId: item.OutcomeId,
                                    OutcomeName: item.OutcomeName,
                                    MatchResultId: betResult.Matches.Where(m => m.MatchId == item.MatchId).Select(m => m.MatchResultId).FirstOrDefault(),
                                    MatchResult: betResult.Matches.Where(m => m.MatchId == item.MatchId).Select(m => m.MatchResult).FirstOrDefault()?? "NotPlayed"
            ));
        }

        
        var result = new MyBetExtende(
                                        BetCardId: betResult.CardId,
                                        CreatedAt: myBets.Value.Select(b => b.CreatedAt).FirstOrDefault(),
                                        MatchSelectionId: betResult.MatchSelectionId,
                                        MatchSelectionName: myBets.Value.Select(b => b.MatchSelectionName).FirstOrDefault()?? "" ,
                                        MatchSelectionDescription: myBets.Value.Select(b => b.MatchSelectionDescription).FirstOrDefault() ?? "",
                                        BetAmount: betResult.BetAmount,
                                        WonAmount: betResult.WonAmount,
                                        TotalWinCount: betResult.TotalWinCount,
                                        MatchType : myBets.Value.Select(b => b.MatchType).FirstOrDefault()!, 
                                        Matches: myBetMatchExtends);                                                                  
        return ErrorOr.ErrorOr.From(result);
        
    }

    public async Task<ErrorOr<MyBetExtende>> Handle2(GetMyBetExtendeResultQuery request, CancellationToken cancellationToken)
    {
        var card = await _context.BetCards.FindAsync(request.BetCardId);
        if(card == null)
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }
        var cardSelections = await _context.BetSelections.Where(c => c.BetCardId == card.BetCardId).ToListAsync();
        if(cardSelections == null)
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }
        var betResult = await _context.BetResults.Where(c => c.MatchTypeId == card.MatchTypeId && c.MatchSelectionId == card.MatchSelectionId).ToListAsync();

        if(betResult == null)
        {
            return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
        }   

        var myBetMatchExtends = new List<MyBetMatchExtend>();

        foreach (var item in betResult)
        {
            var c = cardSelections.Where(c => c.MatchId == item.MatchId).FirstOrDefault();
            
        }   

        return Error.NotFound(code: "BET_CARD_NOT_FOUND", description: "Bet card not found");
    }
}
