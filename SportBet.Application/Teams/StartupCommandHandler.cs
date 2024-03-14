using ErrorOr;
using MediatR;
using SportBet.Contracts.Teams;
using SportBet.Infrastructure;
using SportBet.Application;

namespace SportBet.Application.Teams;

public record StartupCommand : IRequest<ErrorOr<SaveResponse>>
{
    public int CommandId { get; set; }
}
public class StartupCommandHandler : IRequestHandler<StartupCommand, ErrorOr<SaveResponse>>
{
    private readonly BetContext _context;

    public StartupCommandHandler(BetContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<SaveResponse>> Handle(StartupCommand request, CancellationToken cancellationToken)
    {
        if(request.CommandId == 0)
        {
            return ErrorOr.ErrorOr.From(new SaveResponse("Invalid command", false));
        }

        if(request.CommandId == 1)
        {
            await _context.Database.EnsureDeletedAsync(cancellationToken);
            return ErrorOr.ErrorOr.From(new SaveResponse("Deleting Ok", true));
        }
        else
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var utils = new Utils();
            utils.CreateFirstTimeData(_context);

            return ErrorOr.ErrorOr.From(new SaveResponse("Saving OK", true));
        }
    }
}
