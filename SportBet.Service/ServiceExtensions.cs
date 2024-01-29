
using System.Reflection;
using RabbitMQ.Client;
using SportBet.Service.Handler;
using SportBet.Service.Message;
using SportBet.Service.Services;

namespace SportBet.Service;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"));
        service.AddSingleton<IConnection>(sp =>
        {
            var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
            if (rabbitMQSettings == null)
            {
                throw new Exception("RabbitMQSettings section is missing, see appsettings.json");
            }
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQSettings.Host,
                UserName = rabbitMQSettings.Username,
                Password = rabbitMQSettings.Password,
                ClientProvidedName = "RabbitMQ-BetService"
            };

            return factory.CreateConnection();
        });
        service.AddSingleton<RabbitMQChannelFactory>();
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        /*
        service.Configure<HostOptions>(options =>
        {
            options.ServicesStartConcurrently = true;
            options.ServicesStopConcurrently = false;
        });
        service.AddHostedService<Worker>();*/

        service.AddHostedService<ConsumerService>();
        // AddTransient so that I use the logger in the constructor of the Worker class
        service.AddSingleton<MessageConsumer>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<MessageConsumer>>();
            //var channel = sp.GetRequiredService<RabbitMQChannelFactory>();
            return new MessageConsumer(logger);
        });
        return service;
    }
}