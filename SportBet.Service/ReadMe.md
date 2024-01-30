###### Json config file file
```json
"RabbitMQSettings":
{
    "Host": "localhost", //"samtedla.ddns.net", // "192.168.100.2", 
    "Username": "admin",
    "Password": "TheCars010606",
    "QueueName": "Bet-001",
    "ExchangeName": "my.bets-001",
    "RoutingKey": "my-results-001"
}
"RabbitMQSettings":
    {
      "Host": "localhost", //"samtedla.ddns.net", // "192.168.100.2", 
      "Username": "guest",
      "Password": "guest",
      "QueueName": "queue-bets-001",
      "ExchangeName": "exchange-bets-001",
      "RoutingKey": "" //"route-key-bets-001"
    }
```

###### If using MassTransit
MassTrnsit will not consider the exchangeName and routing key, it will use the queue name as the routing key
###### SportBet.Service
```csharp
using MassTransit;
using WithMt;

var builer = WebApplication.CreateBuilder(args);
service.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings")); 
builer.Services.AddMassTransit(x =>
{
    var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
    if (rabbitMQSettings == null)
    {
        throw new Exception("RabbitMQSettings section is missing, see appsettings.json");
    }
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMQSettings.Host, "/", h =>
        {
            h.Username(rabbitMQSettings.Username);
            h.Password(rabbitMQSettings.Password);
        });

        cfg.ReceiveEndpoint("MTrans-One", e =>
        {
            e.ConcurrentMessageLimit = 1;
            e.PrefetchCount = 1;
            e.UseConcurrencyLimit(1);
            e.ExchangeType = ExchangeType.Direct;
            e.Consumer<PingConsumer>();
        })
    });
});
var app = builer.Build();
app.MapGet("/", () => "Hello World!");
// Publisher
app.MapGet("/Publisher", async (IBusControl bus) =>
{
    // Publish a message when the root endpoint is hit
    var randomInteger = new Random().Next(1, 100);
    await bus.Publish(new Ping(Author: $"Samson {randomInteger}", Title: $"This is me ! {randomInteger}"));
    return "Message published!";
});
app.Run();

public record Ping(string Title, string Author);
public class PingConsumer : IConsumer<Ping>
{
    public Task Consume(ConsumeContext<Ping> context)
    {
        Console.WriteLine($"Received: {context.Message.Title} {context.Message.Author}");
        return Task.CompletedTask;
    }
}
```
