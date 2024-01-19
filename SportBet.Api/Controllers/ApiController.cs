using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using SportBet.Application.Authentication.User;

namespace SportBet.Api;

[ApiController]
public class ApiController : ControllerBase
{
    private const string ProblemErrors = "errors-or";
    private IMapper _mapper = null!;
    private ISender _mediator = null!;
    private IPublisher _publisher = null!;
    private IUserContext _userContext = null!;
    protected ISender Sender => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected IPublisher Publisher => _publisher ??= HttpContext.RequestServices.GetRequiredService<IPublisher>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    protected IUserContext UserContext => _userContext ??= HttpContext.RequestServices.GetRequiredService<IUserContext>();
    protected ActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[ProblemErrors] = errors;

        if (errors.TrueForAll(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblems(errors);
        }

        var firstError = errors[0];
        return Problem(firstError);
    }

    private ActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult ValidationProblems(List<Error> errors)
    {
        var modalState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modalState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modalState);
    }
}