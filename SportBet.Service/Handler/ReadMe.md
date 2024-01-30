##### How to use mediator pattern in .NET Core to push messages to RabbitMQ

```csharp
using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SportBet.Service.Message;

namespace SportBet.Service.Handler;
public class BetResultCreatedHandler : IRequestHandler<BetResultCreated, BetResultResponse>
{
    private readonly ILogger<BetResultCreatedHandler> _logger;    
    private readonly RabbitMQChannelFactory _channelFactory;
    private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;

    public BetResultCreatedHandler(ILogger<BetResultCreatedHandler> logger,
        RabbitMQChannelFactory channelFactory,
        IOptions<RabbitMQSettings> rabbitMQSettingsOptions)
    {
        _logger = logger;
        _channelFactory = channelFactory;
        _rabbitMQSettings = rabbitMQSettingsOptions;
    }
    public Task<BetResultResponse> Handle(BetResultCreated request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle : Worker running at: {time}", DateTimeOffset.Now);

        using var channel = _channelFactory.GetChannel();
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

        channel.BasicPublish(exchange: _rabbitMQSettings.Value.ExchangeName,
                            routingKey: _rabbitMQSettings.Value.RoutingKey,
                            basicProperties: null,
                            body: body);

        return Task.FromResult(new BetResultResponse
        {
            BetResultId = 100,
        });
    }
}
```

##### SportBet.Service
```csharp
    int id = 0;
    while (!stoppingToken.IsCancellationRequested)
    {            
        // You can add any other background processing logic here
        CreateMessage(id++, stoppingToken);
        await Task.Delay(5000, stoppingToken);
    }

    private void CreateMessage(int id, CancellationToken cancellationToken)
    {
        var message = new BetResultCreated
        {
            MatchTypeId = RandomNumberGenerator.GetInt32(1, 3),
            MatchSelectionId = RandomNumberGenerator.GetInt32(2, 30),
            MatchId = RandomNumberGenerator.GetInt32(1, 20),
            MatchResultId = id,
        };
        _logger.LogInformation($"BetResultCreated :  {JsonSerializer.Serialize(message)}");
        _mediator.Send(message, cancellationToken);
    }
```