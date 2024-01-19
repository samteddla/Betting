using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SportBet.Api;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ApiController
{
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpDelete]
    [Route("/errors")]
    public IActionResult Errors()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(detail: exception?.Message, type: exception?.GetType().Name);
    }
}