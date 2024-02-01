namespace SportBet.Contracts.Settings;
public class RabbitMqSettings
{
    public const string SectionName = "RabbitMQSettings";
    public string Host { get; set; } = "localhost";
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string QueueName { get; set; } = "my-service";
    public string VirtualHost { get; set; } = "/";
}
