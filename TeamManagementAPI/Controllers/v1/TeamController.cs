using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Application.Abstractions.Repositories;
using Domain.Models.PlayerModels;
using Domain.Models.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Shared.DTO.General.APIAuthentication.Request;
using Shared.DTO.Team.Request.General;
using Shared.DTO.Team.Request.PlayerRequest;
using Shared.DTO.Team.Request.TeamRequests;

namespace TeamManagementAPI.Controllers.v1;

/// <summary>
/// Team Controller init
/// </summary>
[ApiVersion("1.0")]
public class TeamController : BaseController
{
    private readonly ILogger<TeamController> _logger;
    private readonly ITeamServices _team;
    /// <summary>
    /// Team Controller using REST method
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="team"></param>
    public TeamController(ILogger<TeamController> logger, ITeamServices team)
    {
        _logger = logger;
        _team = team;
    }
    /// <summary>
    /// Add a new team to the system
    /// </summary>
    /// <param name="teamInputModel"></param>
    /// <returns>New Team Profile</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("add/new/team")]
    public async Task<IActionResult> AddTeam([FromBody] TeamInputModel teamInputModel)
    {
        try
        {
            string OutPutMessage = string.Empty;
            TeamModel team = null;

            var teamExist = await _team.CheckTeamEmailExist(teamInputModel.Email,teamInputModel.PhoneNumber);
            if (teamExist == 1)
                OutPutMessage = "Email already exist.Please use club email address";
            else if (teamExist == 2)
                OutPutMessage = "Phone number already exist.The phone number is already used by another club";
            else
            {
                team = await _team.AddNewTeam(teamInputModel);
                OutPutMessage = team != null
                    ? "Team registered successfully"
                    : "Failed to register team.Please try again";
            }

            return Ok(new
            {
                IsSuccessful = team != null ? true :false,
                Data = team,
                Message = OutPutMessage
            });
        }
        catch (Exception e)
        {
           _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}\n",
               nameof(AddTeam),JsonConvert.SerializeObject(teamInputModel),e.StackTrace);
           return Ok(new
           {
               IsSuccessful = false,
               Error = "An error occured",
                e.Message,
               StatusCode = 500
           });
        }
    }
    /// <summary>
    /// Login endpoint for the team to access their dashboard
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>The team should be authenticated with authorization</remarks>>
    /// <response code="200">Returns if the team was successfully authenticated</response>
    /// <response code="400">Returns if the team failed to be authenticated or model parsed error</response>
    /// <returns>Team details</returns>
    [HttpPost("login/with/email/and/password")]
    public async Task<IActionResult> LoginWithEmailAndPassword(AuthenticationModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _team.LoginWithEmailAndPassword(model.Username, model.ApiKey);
                return Ok(new
                {
                    IsSuccessful = result != null ? true : false,
                    Data = result,
                    Message = result != null ? "Login successfully":"Failed to login"
                });
            }

            return Ok(new
            {
                IsSuccessful = false,
                Data = model,
                Message = "Model parsed error or the team failed to log in",
                StatusCode = 500
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(LoginWithEmailAndPassword),new{model});
            return Ok(new
            {
                IsSuccessful = false,
                Error = "An error occured at the server end",
                e.Message
            });
        }
    }
    /// <summary>
    /// Get All registered teams in the system
    /// </summary>
    /// <returns>Registered Teams</returns>
    [HttpGet("get/all/teams")]
    public async Task<IActionResult> GetAllTeams()
    {
        try
        {
            var result = await _team.GetAllTeams();
            return Ok(new
            {
                IsSuccessful = result != null ? true : false,
                Data = result,
                Message = result != null ? "Teams data fetched" : "Failed to fetch data",
                Count = result != null ? result.Count : 0
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName} {Traces}\n",
                nameof(GetAllTeams),e.StackTrace);
            return Ok(new
            {
                IsSuccesful = false,
                Error = "Server fatal error",
                e.Message,
                StatusCode = 500
            });
        }
    }

    /// <summary>
    /// Add a new player to the system
    /// </summary>
    /// <param name="playerInputModel"></param>
    /// <returns>Player profile</returns>
    // [HttpPost("add/team/player")]
    // public async Task<IActionResult> AddTeamPlayer(PlayerInputModel playerInputModel)
    // {
    //     try
    //     {
    //         string outputMessage = string.Empty;
    //         Player player = null;
    //         var playerExist = await _team.CheckPlayerExist(playerInputModel.PhoneNumber, playerInputModel.FootballAssociationIdNumber);
    //         switch (playerExist)
    //         {
    //             case 1:
    //                 outputMessage = $"Phone number {playerInputModel.PhoneNumber} already exist";
    //                 break;
    //             case 2:
    //                 outputMessage =
    //                     $"{playerInputModel.FirstName} {playerInputModel.LastName} with code {playerInputModel.FootballAssociationIdNumber} is already registered by another team";
    //                 break;
    //             default:
    //                 player = await _team.AddTeamPlayer(playerInputModel);
    //                 outputMessage = player != null ? "Player added successfully" : "Failed to register player";
    //                 break;
    //         }
    //
    //         return Ok(new
    //         {
    //             IsSuccessful = player != null ? true : false,
    //             Data = player,
    //             Message = outputMessage
    //         });
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}\n",
    //             nameof(AddTeamPlayer),JsonConvert.SerializeObject(playerInputModel),e.StackTrace);
    //         return Ok(new
    //         {
    //             IsSuccessful = false,
    //             Error = "A fatal error has occured from the server",
    //             e.Message,
    //             StatusCode = 500
    //         });
    //     }
    // }
    /// <summary>
    /// Upload player's images request
    /// </summary>
    /// <param name="formCollection"></param>
    /// <returns>Player code</returns>
    [HttpPost("add/player/image")]
    public async Task<IActionResult> AddTeamImage(IFormCollection formCollection)
    {
        try
        {
            string outPutMessage = string.Empty;
            ImageRequest imageRequest = new();
            var teamId = formCollection.FirstOrDefault(r => r.Key.ToLower().Contains("teamId")).Value.ToString();
            var teamRealId = Convert.ToInt32(teamId);
            
            //Implement cloud storage here 
            //Contentful or Google cloud or AWS or Digital Ocean
            
            //change image output request
            imageRequest = await _team.TeamImageRequest(imageRequest);
            return Ok(new
            {
                IsSuccesful = imageRequest != null ? true : false,
                Data = imageRequest.TeamId,
                Message = outPutMessage,
                StatusCode = 200
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    /// <summary>
    /// Get all club partners
    /// </summary>
    /// <returns>All club partners</returns>
    [HttpGet("get/all/team/partners/{teamId}")]
    public async Task<IActionResult> GetAllClubPartners(long teamId)
    {
        try
        {
            string outPutMessage = string.Empty;
            var result = await _team.GetAllClubPartners(teamId);
            return Ok(new
            {
                IsSuccessful = result != null ? true : false,
                Data = result,
                Message = result != null ? "Data fetched successfully" : "Failed to fetch data"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}",
                nameof(GetAllClubPartners),new{teamId},e.StackTrace);
            return Ok(new
            {
                IsSuccessful = false,
                Error = "Fatal error occured",
                e.Message,
                StatusCode = 500
            });
        }
    }
    /// <summary>
    /// Get all players based on the team id provided
    /// Note: teamId(8923923) is to be provided in the request
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns>Team players</returns>
    [HttpGet("get/all/players/{teamId}")]
    public async Task<IActionResult> GetAllPlayers(long teamId)
    {
        try
        {
            var result = await _team.GetAllPlayers(teamId);
            return Ok(new
            {
                IsSuccessful = result.Count > 0,
                Data = result,
                Message = result.Count > 0 ? "Players fetched successfully":"Failed to fetch players",
                Count = result.Count > 0 ? result.Count : 0
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}",
                nameof(GetAllPlayers),new{teamId},e.StackTrace);
            return Ok(new
            {
                IsSuccessful = false,
                Error = "Fatal error occured",
                e.Message,
                StatusCode = 500
            });
        }
    }
    /// <summary>
    /// Get all the team's kits from the system
    /// Note:The teamId e.g teamId(34830948), should be provided
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns>Team's kits</returns>
    [HttpGet("get/all/kits/{teamId}")]
    public async Task<IActionResult> GetAllKits(long teamId)
    {
        try
        {
            var result = await _team.GetAllKits(teamId);
            return Ok(new
            {
                IsSuccessful = result.Count > 0,
                Data = result,
                Message = result.Count > 0 ? "Team kits fetched successfully":"Failed to fetch team kits",
                Count = result.Count > 0 ? result.Count : 0
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e,"An error occured while executing {MethodName}-{Input} {Traces}",
                nameof(GetAllKits),new{teamId},e.StackTrace);
            return Ok(new
            {
                IsSuccessful = false,
                Error = "Fatal error occured",
                e.Message,
                StatusCode = 500
            });
        }
    }
}