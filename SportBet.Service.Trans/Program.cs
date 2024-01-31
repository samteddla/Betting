using MassTransit;
using Microsoft.Extensions.Hosting;
using SportBet.Service.Trans.Consumers;
using Microsoft.Extensions.Configuration;
using SportBet.Contracts.Settings;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMqSettings>();

if (rabbitMQSettings == null)
{
    throw new Exception("RabbitMQSettings section is missing, see appsettings.json");
}

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(busConfigurator =>
    {
        //var entryAssembly = Assembly.GetExecutingAssembly();
        //busConfigurator.AddConsumers(entryAssembly);

        busConfigurator.AddConsumer<BetCreatedConsumer>()
                .Endpoint(e => 
                {
                    e.Name = "bet-created";
                    e.ConcurrentMessageLimit = 2;
                    e.Temporary = false;
                });

        busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
        {
            /*
                // another way of showing data!
                busFactoryConfigurator.ReceiveEndpoint("notify-ceated-queue", e =>{e.ConfigureConsumer<NotificationCreatedConsumer>(context);});
                busFactoryConfigurator.ReceiveEndpoint("user-created-queue", e =>{e.ConfigureConsumer<UserCreatedConsumer>(context);});
            */

            busFactoryConfigurator.Host(rabbitMQSettings.Host, "/", 
            hostConfigurator =>
            {
                hostConfigurator.Username(rabbitMQSettings.Username);
                hostConfigurator.Password(rabbitMQSettings.Password);
            }
            );

            busFactoryConfigurator.ConfigureEndpoints(context);
            busFactoryConfigurator.OverrideDefaultBusEndpointQueueName(rabbitMQSettings.QueueName);
        });
    });
});

var app = builder.Build();
app.Run();