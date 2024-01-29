using System.Security.Cryptography;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using SportBet.Service.Handler;
using SportBet.Service.Message;

namespace SportBet.Service.Services;

public class ConsumerService : BackgroundService
{
    private readonly ILogger<ConsumerService> _logger;
    private readonly IMediator _mediator;
    private readonly RabbitMQChannelFactory _channelFactory;

    public ConsumerService(ILogger<ConsumerService> logger,
        IMediator mediator,
        RabbitMQChannelFactory channelFactory)
    {
        _logger = logger;
        _mediator = mediator;
        _channelFactory = channelFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {      
        _channelFactory.CreateChannel();
        int id = 0;
        while (!stoppingToken.IsCancellationRequested)
        {            
            // You can add any other background processing logic here
            CreateMessage(id++, stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
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
}
