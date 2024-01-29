namespace SportBet.Service.Message;

public class RabbitMQSettings
{
    public string Host { get; set; } = "samtedla.ddns.net";
    public string Username { get; set; } = "admin";
    public string Password { get; set; } = "TheCars010606";
    public string QueueName { get; set; } = "Bet";
    public string ExchangeName { get; set; } = "my.bets";
    public string RoutingKey { get; set; } = "my-results";
}
