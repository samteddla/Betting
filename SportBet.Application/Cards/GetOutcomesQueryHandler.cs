using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;


public record GetOutcomesQuery() : IRequest<ErrorOr<IEnumerable<GetOutcomes>>>;

public class GetOutcomesQueryHandler : IRequestHandler<GetOutcomesQuery, ErrorOr<IEnumerable<GetOutcomes>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetOutcomesQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<GetOutcomes>>> Handle(GetOutcomesQuery request, CancellationToken cancellationToken)
    {
        var query = from o in _context.Outcomes
                    select new
                    {
                        o.Id,
                        o.OutcomeId,
                        o.Name,
                        o.IsEnabled
                    };

        if (!query.Any())
        {
            return Error.NotFound(code: "OUTCOMES_NOT_FOUND", description: "Outcomes not found");
        }

        return ErrorOr.ErrorOr.From(await query.ToListAsync().
        ContinueWith(outcomes => outcomes.Result.Select(o => new GetOutcomes(
            Id: o.Id,
            OutcomeId: o.OutcomeId,
            Name: o.Name,
            IsEnabled: o.IsEnabled
        ))));
    }
}