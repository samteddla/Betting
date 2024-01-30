using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SportBet.Service.Message;

namespace SportBet.Service.Services;

public class RabbitMQChannelFactory : IDisposable
{
    private readonly IConnection _connection;
    private IModel _channel;
    private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;
    private readonly MessageConsumer _messageConsumer;
    public RabbitMQChannelFactory(IConnection connection,IOptions<RabbitMQSettings> rabbitMQSettingsOptions,
        MessageConsumer messageConsumer)
    {
        _connection = connection;
        _rabbitMQSettings = rabbitMQSettingsOptions;
        _messageConsumer = messageConsumer;
        _channel = CreateChannel();
    }

    public IModel GetChannel() 
    { 
        if (_channel != null && _channel.IsOpen)
        {
            return _channel;
        }
        // If the channel is closed or null, create a new one
        _channel = CreateChannel();
        return _channel;
    }

    public IModel CreateChannel()
    {
        var channel = _connection.CreateModel();
        
        channel.ExchangeDeclare(exchange: _rabbitMQSettings.Value.ExchangeName,
                                type: ExchangeType.Direct,
                                durable: true,
                                autoDelete: false,
                                arguments: null);
                                
        channel.QueueDeclare(queue: _rabbitMQSettings.Value.QueueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);    
                             
        channel.QueueBind(queue: _rabbitMQSettings.Value.QueueName,
                            exchange: _rabbitMQSettings.Value.ExchangeName,
                            routingKey: _rabbitMQSettings.Value.RoutingKey);

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        channel.BasicConsume(
            queue: _rabbitMQSettings.Value.QueueName,
            autoAck: true,
            consumerTag: "Bet-Service-Consumer",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: _messageConsumer);

        return channel;
    }

    public void Dispose()
    {
        Close();
    }

    private void Close()
    {
        _channel?.Close();
        _connection?.Close();
    }
}