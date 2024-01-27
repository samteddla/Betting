using ErrorOr;
using MediatR;
using SportBet.Contracts.Selection;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Selections;

public record CreateMatchSelectionsCommand(string Name, string Description, DateTime ActiveUntil, IEnumerable<int> Matches, IEnumerable<int> MatchesTypes) :
         IRequest<ErrorOr<CreateMatchSelectionsResponse>>;

public class CreateMatchSelectionsCommandHandler :
        IRequestHandler<CreateMatchSelectionsCommand, ErrorOr<CreateMatchSelectionsResponse>>
{
    private readonly BetContext _context;

    public CreateMatchSelectionsCommandHandler(BetContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<CreateMatchSelectionsResponse>> Handle(CreateMatchSelectionsCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _context.Database.BeginTransaction();

        // ** get all match types
        var betMatchTypes = request.MatchesTypes.Select(matchType => new BetMatchType
        {
            MatchTypeId = matchType,
            IsEnabled = true
        });

        // 1. create match selection
        var matchSelection = new MatchSelection
        {
            Name = request.Name,
            Description = request.Description,
            ActiveUntil = request.ActiveUntil,
            IsEnabled = true
        };

        try
        {

            await _context.MatchSelections.AddAsync(matchSelection, cancellationToken);
            var ok = await _context.SaveChangesAsync(cancellationToken);
            if (ok == 0)
            {
                transaction.Rollback();
                return Error.Conflict(code: "MATCH_SELECTION_ALREADY_EXISTS", description: "Match selection already exists");
            }

            // 2. add match selection to matchs
            var matchSelectionMatchs = request.Matches.Select(match => new MatchSelectionMatch
            {
                Selection = matchSelection,
                MatchId = match
            });

            await _context.MatchSelectionMatches.AddRangeAsync(matchSelectionMatchs, cancellationToken);
            ok = await _context.SaveChangesAsync(cancellationToken);
            if (ok == 0)
            {
                transaction.Rollback();
                return Error.Conflict(code: "MATCH_SELECTION_ALREADY_EXISTS", description: "Match selection already exists");
            }

            // 3. add betresults to match selection
            var bestResultsWithMachType = betMatchTypes.SelectMany(matchType => matchSelectionMatchs.Select(match => new BetResult
            {
                MatchId = match.MatchId,
                MatchSelectionId = matchSelection.MatchSelectionId,
                Outcome = 0,
                MatchTypeId = matchType.MatchTypeId
            }));

            // Save all the above (1-3)
            await _context.BetResults.AddRangeAsync(bestResultsWithMachType, cancellationToken);
            ok = await _context.SaveChangesAsync(cancellationToken);

            if (ok == 0)
            {
                transaction.Rollback();
                return Error.Conflict(code: "MATCH_SELECTION_ALREADY_EXISTS", description: "Match selection already exists");
            }

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            transaction.Commit();
            transaction.Dispose();
        }

        return ErrorOr.ErrorOr.From(new CreateMatchSelectionsResponse(
                                                                        MatchSelectionId: matchSelection.MatchSelectionId,
                                                                        StatusName: "OK",
                                                                        IsSaved: true
                                                                        ));
    }
}