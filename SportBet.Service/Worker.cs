
using MediatR;
using Microsoft.Extensions.Options;
using SportBet.Service.Message;
using SportBet.Service.Services;

namespace SportBet.Service;

public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    private readonly RabbitMQChannelFactory _channelFactory;
    private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;

    public Worker(ILogger<Worker> logger, IMediator mediator,
        RabbitMQChannelFactory channelFactory,
        IOptions<RabbitMQSettings> rabbitMQSettingsOptions)
    {
        _channelFactory = channelFactory;
        _rabbitMQSettings = rabbitMQSettingsOptions;
        _mediator = mediator;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartAsync : Worker running at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartedAsync (BasicConsume): Worker running at: {time}", DateTimeOffset.Now);

        
        using var channel = _channelFactory.CreateChannel();
        /*var consumer = new MessageConsumer();
        // string queue, bool autoAck, string consumerTag, bool noLocal, bool exclusive, IDictionary<string, object> arguments, IBasicConsumer consumer
        channel.BasicConsume(
            queue: _rabbitMQSettings.Value.QueueName, 
            autoAck: true,
            consumerTag: "",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer);*/

        /*
        while(!cancellationToken.IsCancellationRequested)
        {
           // You can add any other background processing logic here
           _logger.LogInformation("StartedAsync (BasicConsume): Worker running at: {time}", DateTimeOffset.Now);
           
           var message = new BetResultCreated
            {
                MatchTypeId = RandomNumberGenerator.GetInt32(1, 3),
                MatchSelectionId = RandomNumberGenerator.GetInt32(2, 30),
                MatchId = RandomNumberGenerator.GetInt32(1, 20),
                MatchResultId = 2,
            };
          _mediator.Send(message, cancellationToken);
           Task.Delay(30000, cancellationToken);
        }*/

        return Task.CompletedTask;
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartingAsync : Worker running at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StopAsync : Worker running at: {time}", DateTimeOffset.Now);

        var message = new BetResultCreated
            {
                MatchTypeId = 200,
                MatchSelectionId = 100,
                MatchId = 200,
                MatchResultId = 200,
            };
        _mediator.Send(message, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppedAsync : Worker running at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppingAsync : Worker running at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;

    }
}
