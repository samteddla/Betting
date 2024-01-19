using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Games;
using SportBet.Infrastructure;

namespace SportBet.Application.Games;

public record GetActivBetsQuery() : IRequest<ErrorOr<IEnumerable<GetActivBetsResponse>>>;

public class GetActivBetsQueryHandler : IRequestHandler<GetActivBetsQuery, ErrorOr<IEnumerable<GetActivBetsResponse>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetActivBetsQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<GetActivBetsResponse>>> Handle(GetActivBetsQuery request, CancellationToken cancellationToken)
    {
        var personId = _userContext.UserId;
        var sql = $@"select * from BetCard b
                    left join MatchSelection ms on b.MatchSelectionId = ms.MatchSelectionId
                    left join BetMatchType m on b.MatchTypeId = m.MatchTypeId
                    where b.PersonId = 1";

        var query = from b in _context.BetCards
                    join ms in _context.MatchSelections on b.MatchSelectionId equals ms.MatchSelectionId
                    join m in _context.MatchTypes on b.MatchTypeId equals m.MatchTypeId
                    where b.PersonId == personId
                    select new
                    {
                        b.BetCardId,
                        ms.Name,
                        ms.Description,
                        m.IsEnabled,
                        TypeName = m.Name
                    };

        var ret = await query.ToListAsync(cancellationToken)
                    .ContinueWith(t => t.Result.Select(c => new GetActivBetsResponse(
                                                        Id: c.BetCardId,
                                                        Name: c.Name,
                                                        Description: c.Description,
                                                        TypeName: c.TypeName,
                                                        IsEnabled: c.IsEnabled
        )));

        return ErrorOr.ErrorOr.From(ret);

    }
}