using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMatchTypesQuery() : IRequest<ErrorOr<IEnumerable<GetMatchTypes>>>;
public class GetMatchTypesQueryHandler : IRequestHandler<GetMatchTypesQuery, ErrorOr<IEnumerable<GetMatchTypes>>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetMatchTypesQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<GetMatchTypes>>> Handle(GetMatchTypesQuery request, CancellationToken cancellationToken)
    {
        var query = from mt in _context.MatchTypes
                    select new
                    {
                        mt.MatchTypeId,
                        mt.Name
                    };

        if (!query.Any())
        {
            return Error.NotFound(code: "MATCH_TYPES_NOT_FOUND", description: "Match types not found");
        }

        return ErrorOr.ErrorOr.From(
            await query.ToListAsync().ContinueWith(matchTypes => matchTypes.Result.Select(mt => new GetMatchTypes(
                MatchTypeId: mt.MatchTypeId,
                Name: mt.Name
            ))));
    }
}