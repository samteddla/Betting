using MassTransit;
using Microsoft.Extensions.Hosting;
using SportBet.Service.Trans.Consumers;
using Microsoft.Extensions.Configuration;
using SportBet.Contracts.Settings;
using Serilog;
using Serilog.Events;

Log.Logger = new Serilog.LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog();

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
            e.Name = rabbitMQSettings.QueueName;
            e.ConcurrentMessageLimit = 2;
            e.Temporary = false;
        });

        busConfigurator.AddConsumer<MessageCreatedConsumer>()
        .Endpoint(e =>
        {
            e.Name = "consumer-created";
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

            busFactoryConfigurator.Host(rabbitMQSettings.Host, rabbitMQSettings.VirtualHost,
            hostConfigurator =>
            {
                hostConfigurator.Username(rabbitMQSettings.Username);
                hostConfigurator.Password(rabbitMQSettings.Password);
            }
            );

            /*
            busFactoryConfigurator.ReceiveEndpoint("bet-created-queue", e =>
            {
                e.BindQueue = true; // Prevent automatic exchange binding
                e.Bind("bet-created-exchange");
                e.Consumer<BetCreatedConsumer>();
            });*/

            busFactoryConfigurator.ConfigureEndpoints(context);
            // busFactoryConfigurator.OverrideDefaultBusEndpointQueueName(rabbitMQSettings.QueueName);
        });
    });
});
/*
builder.Services.AddOptions<MassTransitHostOptions>()
.Configure(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromMinutes(1);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});
builder.Services.AddOptions<HostOptions>()
.Configure(options => options.ShutdownTimeout = TimeSpan.FromMinutes(1));
*/
var app = builder.Build();
app.Run();