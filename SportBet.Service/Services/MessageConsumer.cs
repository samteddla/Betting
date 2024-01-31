using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SportBet.Service.Services;
public class MessageConsumer(ILogger<MessageConsumer> logger) : IBasicConsumer
{
    private readonly ILogger<MessageConsumer> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public event EventHandler<ConsumerEventArgs>? ConsumerCancelled; // Add a question mark to make the event nullable
    public IModel Model => throw new NotImplementedException();

    public void HandleBasicDeliver(string consumerTag,
                                   ulong deliveryTag,
                                   bool redelivered,
                                   string exchange,
                                   string routingKey,
                                   IBasicProperties properties,
                                   ReadOnlyMemory<byte> body)
    {
        try
        {
            var message = Encoding.UTF8.GetString(body.ToArray());

            _logger.LogInformation(
                                $@"Received message with props :
                                consumerTag : {consumerTag} 
                                deliveryTag : {deliveryTag}
                                redelivered : {redelivered}
                                exchange : {exchange}
                                routingKey : {routingKey}
                                body : {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HandleBasicDeliver");
        }
    }
    
    public void HandleModelShutdown(object model, ShutdownEventArgs reason)
    {
        ConsumerCancelled?.Invoke(this, new ConsumerEventArgs(["Hello", "World"]));
        _logger.LogInformation($"HandleModelShutdown: {reason.ReplyText}");
    }
    public void HandleBasicCancel(string consumerTag)
    {
        _logger.LogInformation($"ConsumerTag HandleBasicCancel: {consumerTag}");
    }

    public void HandleBasicCancelOk(string consumerTag)
    {
        _logger.LogInformation($"ConsumerTag HandleBasicCancelOk: {consumerTag}");
    }

    public void HandleBasicConsumeOk(string consumerTag)
    {
        _logger.LogInformation($"consumerTag HandleBasicConsumeOk: {consumerTag}");
    }
}