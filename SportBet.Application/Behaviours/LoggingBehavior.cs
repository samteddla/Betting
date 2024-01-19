using MediatR;

using Microsoft.Extensions.Logging;


namespace SportBet.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly ILogger<TRequest> _logger;
    private readonly Action<ILogger, string, Exception> Log = LogRequestResponse();

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).FullName;
        Log(_logger, $"REQUEST TYPE : {requestName}", null!);

        var response = await next();

        var responseName = typeof(TResponse).FullName;
        Log(_logger, $"RESPONSE TYPE : {responseName}", null!);

        return response;
    }

    private static Action<ILogger, string, Exception> LogRequestResponse()
    {
        return LoggerMessage.Define<string>(LogLevel.Information, eventId: new EventId(id: 2, name: "Request-Response"), formatString: "{Message}");
    }
}