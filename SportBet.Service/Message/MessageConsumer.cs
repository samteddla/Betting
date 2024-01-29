using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SportBet.Service.Message;
public class MessageConsumer : IBasicConsumer
{
    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(ILogger<MessageConsumer> logger)
    {
        _logger = logger;
    }

    public IModel Model => throw new NotImplementedException();

    public event EventHandler<ConsumerEventArgs>? ConsumerCancelled;

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
            // Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            if (routingKey != "betresult.created")
            {            
                //using var channel = _channelFactory.CreateChannel();
                //channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
            }
            var message = Encoding.UTF8.GetString(body.ToArray());

            _logger.LogInformation($@"Received message with props :
                                        consumerTag : {consumerTag} 
                                        deliveryTag : {deliveryTag}
                                        redelivered : {redelivered}
                                        exchange : {exchange}
                                        routingKey : {routingKey}
                                        body : {message}");

            // Task.Delay(TimeSpan.FromSeconds(20)).Wait();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HandleBasicDeliver");
        }

    }

    public void HandleBasicCancel(string consumerTag)
    {
        ConsumerCancelled?.Invoke(this, new ConsumerEventArgs(new string[] { consumerTag }));
        _logger.LogInformation($"ConsumerTag HandleBasicCancel: {consumerTag}");
    }

    public void HandleBasicCancelOk(string consumerTag)
    {
        _logger.LogInformation($"ConsumerTag HandleBasicCancelOk: {consumerTag}");
    }

    public void HandleBasicConsumeOk(string consumerTag)
    {
        _logger.LogInformation($"consumerTag HandleBasicConsumeOk Ok: {consumerTag}");
    }
    public void HandleModelShutdown(object model, ShutdownEventArgs reason)
    {
        _logger.LogInformation($"HandleModelShutdown: {reason.ReplyText}");
    }
}