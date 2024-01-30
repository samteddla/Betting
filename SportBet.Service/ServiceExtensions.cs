
using System.Reflection;
using RabbitMQ.Client;
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
                ClientProvidedName = "Bet-Service"
            };

            return factory.CreateConnection();
        });
        service.AddSingleton<RabbitMQChannelFactory>();
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        // AddTransient so that I use the logger in the constructor of the Worker class
        service.AddTransient<MessageConsumer>();
        service.AddHostedService<ConsumerService>();
        return service;
    }
}