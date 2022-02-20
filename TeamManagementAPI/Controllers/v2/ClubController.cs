using Domain.Models.Team;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Team.Request.TeamRequests;
using TeamManagement.Services.v1.Commands;
using TeamManagement.Services.v1.Query;

namespace TeamManagementAPI.Controllers.v2;

[ApiVersion("2.0")]
public class ClubController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ClubController> _logger;

    public ClubController(ILogger<ClubController> logger,IMediator mediator)
    {
        _logger = _logger;
        _mediator = mediator;
    }
    /// <summary>
    /// Creates a new club 
    /// </summary>
    /// <param name="player"></param>
    /// <response code="200">Return if the club was created</response>
    /// <response code="400">Return if the club was not model was not able to parse or the club wouldn't be saved</response>
    /// <returns>Returns the new created club</returns>
    [HttpPost("add/new/club")]
    public async Task<IActionResult> AddNewTeam(TeamInputModel player)
    {
        try
        {
            var result = await _mediator.Send(new CreateTeamCommand
            {
                Team = player
            });
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Returns all Clubs in the system
    /// </summary>
    /// <returns></returns>
    [HttpGet("get/all/teams")]
    public async Task<IActionResult> GetAllTeams()
    {
        try
        {
            var result = await _mediator.Send(new GetAllTeamsQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName} {Traces}",
                nameof(GetAllTeams),e.StackTrace);
            return Ok(new
            {
                Successful = false,
                Error = "An error occured at the server",
                e.Message,
                StatusCode = 500
            });
        }
    }
    /// <summary>
    /// Add new team kit
    /// </summary>
    /// <param name="clubKitsModel"></param>
    /// <returns>Team Kit</returns>
    [HttpPost("add/team/kit")]
    public async Task<IActionResult> AddTeamKit(ClubKitsModel clubKitsModel)
    {
        try
        {
            var result = await _mediator.Send(new AddClubKitCommand
            {
                ClubKit = clubKitsModel
            });
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName} {Traces}",
                nameof(AddTeamKit),e.StackTrace);
            return Ok(new
            {
                Successful = false,
                Error = "An error occured at the server",
                e.Message,
                StatusCode = 500
            });
        }
    }
}