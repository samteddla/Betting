namespace SportBet.Contracts.Users;

public record AddUserRequest(string Username, string Password, string FirstName, string MiddleName, string LastName, string PhoneNumber, string Email);
public record AddUserResponse(int Id, string StatusName);

public record UpdateUserRequest(int Id, string Username, string Password, string FirstName, string MiddleName, string LastName, string PhoneNumber, string Email);
public record UpdateUserResponse(int Id, string StatusName);

public record ChangePasswordRequest(string Password, string NewPassword);
public record ChangePasswordResponse(int Id, string StatusName);