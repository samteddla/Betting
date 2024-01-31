using MassTransit;
using SportBet.Contracts.Interfaces;
using System.Text.Json;

namespace SportBet.Service.Trans.Consumers;

public class BetCreatedConsumer : IConsumer<IBetCreated>
{
    public async Task Consume(ConsumeContext<IBetCreated> context)
    {
        try
        {
            var serializedMessage = await Task.Run(() => JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { }));
            Console.WriteLine($@"BetCreated event consumed 
                                Message: 
                                {serializedMessage}");
            ConsoleLog(context);

        }
        catch (Exception ex)
        {
            Console.WriteLine($@"Error: {ex.Message}");
            throw;
        }
    }

    private void ConsoleLog(ConsumeContext<IBetCreated> context)
    {
        Console.WriteLine($@"   FaultAddress : {context.FaultAddress}
                                InputAddress : {context.ResponseAddress}
                                SupportedMessageTypes : {context.SupportedMessageTypes}
                                RoutingKey : {context.RoutingKey()}
                                DestinationAddress : {context.DestinationAddress}
                                SourceAddress : {context.SourceAddress}
                                ConversationId : {context.ConversationId}
                                CorrelationId : {context.CorrelationId}
                                MessageId : {context.MessageId}
                                RequestId : {context.RequestId}
                                SentTime : {context.SentTime}
                                Headers : {JsonSerializer.Serialize(context.Headers, new JsonSerializerOptions { })}
                                Host : {JsonSerializer.Serialize(context.Host, new JsonSerializerOptions { })}
                                SerializerContext : {JsonSerializer.Serialize(context.SerializerContext, new JsonSerializerOptions { })}");

    }
}