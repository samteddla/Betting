using ErrorOr;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using SportBet.Domain.Common;
using System.Diagnostics;
using System.Text.Json;

namespace SportBet.Api.Common;

public class ApiProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly ILogger<ApiProblemDetailsFactory> _logger;
    private readonly Action<ILogger, string, Exception> Log = LogValidationProblem();
    private static Action<ILogger, string, Exception> LogValidationProblem()
    {
        return LoggerMessage.Define<string>(LogLevel.Error, eventId: new EventId(id: 1, name: "Validation-Problem"), formatString: "{Message}");
    }

    public ApiProblemDetailsFactory(IOptions<ApiBehaviorOptions> options, ILogger<ApiProblemDetailsFactory> logger)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
                                                        int? statusCode = null,
                                                        string? title = null,
                                                        string? type = null,
                                                        string? detail = null,
                                                        string? instance = null)
    {
        statusCode ??= 500;
        ProblemDetails? problemDetails = null;
        var context = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (context?.Error != null && context.Error is ApiException myError)
        {
            statusCode = 400;
            httpContext.Response.StatusCode = statusCode.Value;
            problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = myError.Title,
                Type = myError.ErrorType,
                Detail = myError.Message,
                Instance = instance,
                Extensions = { { "ext-Info", myError.ExtendedInfo } }
            };
        }

        //	default exception handler
        problemDetails ??= new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetails(httpContext, problemDetails, statusCode.Value, true);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
                                                                            ModelStateDictionary modelStateDictionary,
                                                                            int? statusCode = null,
                                                                            string? title = null,
                                                                            string? type = null,
                                                                            string? detail = null,
                                                                            string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);
        statusCode ??= 400;
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };
        if (title != null)
        {
            problemDetails.Title = title;
        }

        ApplyProblemDetails(httpContext, problemDetails, statusCode.Value, false);

        var errorDetails = JsonSerializer.Serialize(problemDetails);
        Log(_logger, "ValidationError : " + errorDetails, null!);

        return problemDetails;
    }

    private void ApplyProblemDetails(HttpContext httpContext, ProblemDetails problemDetails, int statusCode, bool isProblem)
    {
        problemDetails.Status ??= statusCode;
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        if (isProblem)
        {
            var errors = httpContext?.Items["errors-or"] as List<Error>;
            if (errors is not null)
            {
                if (problemDetails.Extensions.ContainsKey("errors"))
                {
                    problemDetails.Extensions.Remove("errors");
                }
                problemDetails.Extensions.Add("errors", errors.Select(e => e.Code + " : " + e.Description).ToList());
            }
        }

        problemDetails.Extensions.Add("api", "SportBet.Api");
    }
}