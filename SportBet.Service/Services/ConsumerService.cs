namespace SportBet.Service.Services;

public class ConsumerService(RabbitMQChannelFactory channelFactory) : BackgroundService
{
    private readonly RabbitMQChannelFactory _channelFactory = channelFactory;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) => await Task.Run(() => _channelFactory.CreateChannel());
    public override async Task StopAsync(CancellationToken stoppingToken) => await base.StopAsync(stoppingToken);
    public override async Task StartAsync(CancellationToken cancellationToken) => await base.StartAsync(cancellationToken);
}
