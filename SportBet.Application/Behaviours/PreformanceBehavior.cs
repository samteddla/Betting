using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SportBet.Application.Behaviours;

public class PreformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
   where TResponse : IErrorOr
{
    private readonly Action<ILogger, string, Exception> Log = LogPerformance();
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;

    public PreformanceBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
        _timer = new Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();
        var elapsedMs = _timer.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;

        if (elapsedMs > 500)
        {
            Log(_logger, $"LONG-RUNNING-REQUEST: {requestName} ({elapsedMs} milliseconds)", null!);
        }

        return response;
    }

    private static Action<ILogger, string, Exception> LogPerformance()
    {
        return LoggerMessage.Define<string>(LogLevel.Warning, eventId: new EventId(id: 2, name: "Preformance"), formatString: "{Message}");
    }
}