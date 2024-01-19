namespace SportBet.Application.Authentication.User;

public interface IUserContext
{
    int UserId { get; }
    string Email { get; }
    string UserName { get; }
    string Role { get; }
    string Phone { get; }
}