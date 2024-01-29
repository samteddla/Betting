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
    private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _serviceProvider;
    private readonly RabbitMQChannelFactory _channelFactory;


    public ConsumerService(ILogger<ConsumerService> logger,
        IOptions<RabbitMQSettings> rabbitMQSettingsOptions,
        IMediator mediator,
        IServiceScopeFactory serviceProvider,
        RabbitMQChannelFactory channelFactory)
    {
        _logger = logger;
        _rabbitMQSettings = rabbitMQSettingsOptions;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
        _channelFactory = channelFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {      
        _channelFactory.CreateChannel();  
        /*
        var scope = _serviceProvider.CreateScope();
        var consumer =
            scope.ServiceProvider
                .GetRequiredService<MessageConsumer>();

        using var channel = scope.ServiceProvider
                .GetRequiredService<RabbitMQChannelFactory>().CreateChannel();

        channel.BasicConsume(
            queue: _rabbitMQSettings.Value.QueueName,
            autoAck: false,
            consumerTag: "This is Another FUN!",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer);

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        */
        int id = 0;
        while (!stoppingToken.IsCancellationRequested)
        {            
            // You can add any other background processing logic here
            // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);            
           // CreateMessage(id++, stoppingToken);
            await Task.Delay(1000, stoppingToken);
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
