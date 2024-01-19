using ErrorOr;
using MediatR;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record BetOnSelectionCommand(int SelectionId, int MatchTypeId, List<MatchRequest> Matches, decimal Amount) : IRequest<ErrorOr<BetOnGameResponse>>;

public class BetOnSelectionCommandHandler : IRequestHandler<BetOnSelectionCommand, ErrorOr<BetOnGameResponse>>
{
    private readonly BetContext _context;
    private readonly IUserContext _userContext;

    public BetOnSelectionCommandHandler(BetContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ErrorOr<BetOnGameResponse>> Handle(BetOnSelectionCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var betCard = new BetCard
        {
            PersonId = _userContext.UserId,
            MatchSelectionId = request.SelectionId,
            BetAmount = request.Amount,
            BetDate = DateTime.Now,
            MatchTypeId = request.MatchTypeId
        };

        await _context.BetCards.AddAsync(betCard, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var betSelections = request.Matches.Select(m => new BetSelection
        {
            BetCardId = betCard.BetCardId,
            MatchId = m.MatchId,
            Outcome = m.OutcomeId
        });

        await _context.BetSelections.AddRangeAsync(betSelections, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return ErrorOr.ErrorOr.From(new BetOnGameResponse(
            StatusName: "OK",
            IsSaved: true
        ));
    }
}