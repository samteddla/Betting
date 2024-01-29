
using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

        using var channel = _channelFactory.CreateChannel();
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
