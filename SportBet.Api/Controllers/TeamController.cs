using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBet.Application.Teams;
using SportBet.Contracts.Teams;

namespace SportBet.Api;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TeamController : ApiController
{
    // get teams
    [HttpGet(Name = "GetTeams")]
    public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTeams()
    {
        var teams = await Sender.Send(new GetTeamsQuery());

        return teams.Match(
           base.Ok,
           Problem);
    }

    [HttpPost(Name = "AddTeam")]
    public async Task<ActionResult<TeamResponse>> AddTeam(AddTeamCommand command)
    {
        var team = await Sender.Send(command);
        return team.Match(
           base.Ok,
           Problem);
    }

    
    [HttpGet(Name = "Startup/{commandId}")]
    [AllowAnonymous]
    public async Task<ActionResult<SaveResponse>> Startup(int commandId = 0)
    {
        var response = await Sender.Send(new StartupCommand{
            CommandId = commandId
        });

        return response.Match(
           base.Ok,
           Problem);
    }
}
