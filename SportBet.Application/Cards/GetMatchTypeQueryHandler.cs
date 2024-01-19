using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Cards;
using SportBet.Infrastructure;

namespace SportBet.Application.Cards;

public record GetMatchTypeQuery(int MatchTypeId) : IRequest<ErrorOr<GetMatchTypes>>;
public class GetMatchTypeQueryHandler : IRequestHandler<GetMatchTypeQuery, ErrorOr<GetMatchTypes>>
{
    private readonly IUserContext _userContext;
    private readonly BetContext _context;

    public GetMatchTypeQueryHandler(IUserContext userContext, BetContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<ErrorOr<GetMatchTypes>> Handle(GetMatchTypeQuery request, CancellationToken cancellationToken)
    {
        var query = from mt in _context.MatchTypes
                    where mt.MatchTypeId == request.MatchTypeId
                    select new
                    {
                        mt.MatchTypeId,
                        mt.Name
                    };

        if (!query.Any())
        {
            return Error.NotFound(code: "MATCH_TYPES_NOT_FOUND", description: "Match types not found");
        }

        var result = await query.ToListAsync().ContinueWith(matchTypes => matchTypes.Result.Select(mt => new GetMatchTypes(
                MatchTypeId: mt.MatchTypeId,
                Name: mt.Name
            )))
            .ContinueWith(matchTypes => matchTypes.Result.FirstOrDefault());

        if (result == null)
        {
            return Error.NotFound(code: "MATCH_TYPES_NOT_FOUND", description: "Match types not found");
        }

        return ErrorOr.ErrorOr.From(result);
    }
}