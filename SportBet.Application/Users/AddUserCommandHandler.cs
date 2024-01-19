using ErrorOr;
using MediatR;
using SportBet.Contracts.Users;
using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application.Users;

public record AddUserCommand(string Username, string Password, string FirstName, string MiddleName, string LastName, string PhoneNumber, string Email) : IRequest<ErrorOr<AddUserResponse>>;


public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<AddUserResponse>>
{
    private readonly BetContext _context;

    public AddUserCommandHandler(BetContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<AddUserResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Person
        {
            UserName = request.Username,
            Password = request.Password,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Roles = "User"
        };

        _context.People.Add(user);

        await _context.SaveChangesAsync();

        return ErrorOr.ErrorOr.From(new AddUserResponse(user.PersonId, "User added"));
    }
}