using ErrorOr;
using MediatR;
using SportBet.Contracts.Teams;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Teams;

public record StartupCommand : IRequest<ErrorOr<SaveResponse>>;
public class StartupCommandHandler : IRequestHandler<StartupCommand, ErrorOr<SaveResponse>>
{
    private readonly BetContext _context;

    public StartupCommandHandler(BetContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<SaveResponse>> Handle(StartupCommand request, CancellationToken cancellationToken)
    {
        var outcomes = new List<Outcome>
            {
                new () { OutcomeId = 1, Name = "Win home"},
                new () { OutcomeId = 2, Name = "Win away"},
                new () { OutcomeId = 4, Name = "Draw"},
                new () { OutcomeId = 5, Name = "Win home or draw"},
                new () { OutcomeId = 3, Name = "Win home or away"},
                new () { OutcomeId = 6, Name = "Win away or draw"},
                new () { OutcomeId = 7, Name = "Win home or away or draw"}
            };
        var outcameExisits = _context.Outcomes;
        if (outcameExisits.Any())
        {
            _context.Outcomes.RemoveRange(outcameExisits);
        }
        _context.Outcomes.AddRange(outcomes);
        _context.SaveChanges();

        await CreateAccounting();

        return ErrorOr.ErrorOr.From(new SaveResponse("OK", true));
    }

    private Task CreateAccounting()
    {
        throw new NotImplementedException();
    }
}
