using ErrorOr;
using MediatR;
using SportBet.Application.Authentication.User;
using SportBet.Contracts.Users;
using SportBet.Infrastructure;

namespace SportBet.Application.Users;

public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<ErrorOr<ChangePasswordResponse>>;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<ChangePasswordResponse>>
{
    private readonly BetContext _context;
    private readonly IUserContext _userContext;

    public ChangePasswordCommandHandler(BetContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ErrorOr<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.People.FindAsync(_userContext.UserId);

        if (user == null)
        {
            return Error.NotFound(code: "USER_NOT_FOUND", description: "User not found");
        }

        if (user.Password != request.Password)
        {
            return Error.Failure(code: "PASSWORD_NOT_MATCH", description: "Password not match");
        }

        user.Password = request.NewPassword;

        await _context.SaveChangesAsync();

        return ErrorOr.ErrorOr.From(new ChangePasswordResponse(user.PersonId, "Password changed"));
    }
}