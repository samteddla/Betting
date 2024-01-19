using ErrorOr;
using MediatR;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Users;
using SportBet.Infrastructure;

namespace SportBet.Application.Users;

public record UpdateUserCommand(string Username, string Password, string FirstName, string MiddleName, string LastName, string PhoneNumber, string Email) : IRequest<ErrorOr<UpdateUserResponse>>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UpdateUserResponse>>
{
    private readonly BetContext _context;
    private readonly IUserContext _userContext;

    public UpdateUserCommandHandler(BetContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ErrorOr<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.People.FindAsync(_userContext.UserId);

        if (user == null)
        {
            return Error.NotFound(code: "USER_NOT_FOUND", description: "User not found");
        }

        user.UserName = request.Username;
        user.Password = request.Password;
        user.FirstName = request.FirstName;
        user.MiddleName = request.MiddleName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Email = request.Email;

        await _context.SaveChangesAsync();

        return ErrorOr.ErrorOr.From(new UpdateUserResponse(user.PersonId, "User updated"));
    }
}