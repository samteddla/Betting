### common interface
Project file:
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="MassTransit" Version="8.0.1" />
    <PackageReference Include="MassTransit.RabbitMQ" Version="8.0.1" />
  </ItemGroup>
</Project>
```
Configuration appsettings.json
```json
"RabbitMQSettings":
{
    "Host": "localhost",
    "Username": "admin",
    "Password": "admin123456",
    "QueueName": "sportbet-api-queue",
    "VirtualHost": "/test"  
},
```
```csharp
namespace Contract.Interfaces;
public class RabbitMqSettings
{
    public const string SectionName = "RabbitMQSettings";
    public string Host { get; set; } = "localhost";
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string QueueName { get; set; } = "my-service";
    public string VirtualHost { get; set; } = "/";
}

public interface IMessageCreated
{
    int MessageId {get;}
}
```

##### Publish 

```csharp
private readonly IPublishEndpoint _publishEndpoint;
// or
private readonly ISendEndpointProvider _sendEndpointProvider;

public class MyPusher : BaseController
{
    public class MyPusher(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public PushMethod()
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:consumer-created"));
        await endpoint.Send<IMessageCreated>(new { MessageId = 10020  });

        await _publishEndpoint.Publish<IMessageCreated>(new { MessageId = 20010 });
    }
}
```
program.cs
```csharp
public static IServiceCollection AddMassTransitWithRabitMq(this IServiceCollection services, ConfigurationManager configuration)
{
    var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.SectionName).Get<RabbitMqSettings>();

    if (rabbitMqSettings == null)
    {
        throw new Exception("RabbitMQSettings section is missing, see appsettings.json");
    }

    services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, hostConfigurator =>
            {
                hostConfigurator.Username(rabbitMqSettings.Username);
                hostConfigurator.Password(rabbitMqSettings.Password);
            });
            cfg.OverrideDefaultBusEndpointQueueName(rabbitMqSettings.QueueName);
        });
        // busConfigurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "Dev", includeNamespace: false));
    });

    return services;
}
```

#### Consumer
Project file:
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include="MassTransit" Version="8.0.1" />
        <PackageReference Include="MassTransit.RabbitMQ" Version="8.0.1" />
    </ItemGroup>
</Project>
```
Consumer class:
```csharp
public class MessageCreatedConsumer : IConsumer<IMessageCreated>
{
    public Task Consume(ConsumeContext<IMessageCreated> context)
    {
        var serializedMessage = await Task.Run(() => JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { }));
        Console.WriteLine($@"BetCreated event consumed Message: {serializedMessage}");            
    }
}
```
program.cs

```csharp
var builder = Host.CreateDefaultBuilder(args);
var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMqSettings>();

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(busConfigurator =>
    {
        //  manually configured receive endpoint or use default under UsingRabbitMq > cfg.ConfigureEndpoints(context);
        busConfigurator.AddConsumer<MessageCreatedConsumer>()
        .Endpoint(e =>
        {
            e.Name = "consumer-created"; // Queue-name // not recomended!
            e.ConcurrentMessageLimit = 2;
            e.Temporary = false;
            e.UseMessageRetry(r => r.Immediate(5)); // 
            e.Mandatory = true;
        });
        busConfigurator.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.VirtualHost,
                hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMQSettings.Username);
                    hostConfigurator.Password(rabbitMQSettings.Password);
                }
            );
            // for whole bus
            cfg.UseMessageRetry(r => r.Immediate(5));
            // recommended way
            cfg.ConfigureEndpoints(context);
        }
    }
}
var app = builder.Build();
app.Run();
```
##### RabitMq config file to import
```json
{
    "rabbit_version": "3.12.12",
    "rabbitmq_version": "3.12.12",
    "product_name": "RabbitMQ",
    "product_version": "3.12.12",
    "users": [
        {
            "name": "admin",
            "password_hash": "SlH/gN2210A9H2pctI9EwT0nq+2RTc4BVxDkCrUu0+1k6yZf",
            "hashing_algorithm": "rabbit_password_hashing_sha256",
            "tags": [
                "administrator"
            ],
            "limits": {}
        }
    ],
    "vhosts": [
        {
            "name": "/test"
        },
        {
            "name": "/"
        }
    ],
    "permissions": [
        {
            "user": "admin",
            "vhost": "/test",
            "configure": ".*",
            "write": ".*",
            "read": ".*"
        },
        {
            "user": "admin",
            "vhost": "/",
            "configure": ".*",
            "write": ".*",
            "read": ".*"
        }
    ],
    "topic_permissions": [],
    "parameters": [],
    "global_parameters": [
        {
            "name": "internal_cluster_id",
            "value": "rabbitmq-cluster-id-xb0b5FYDofhcG44LVX8iZg"
        }
    ],
    "policies": [],
    "queues": [],
    "exchanges": [],
    "bindings": []
}
``` 
For more advanced code see : https://github.com/g2384/Sample-JobConsumers
