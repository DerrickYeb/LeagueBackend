using System.Reflection;
using Domain.Models;
using Domain.Models.PlayerModels;
using Domain.Models.Team;
using Shared.DTO.Team.Request;
using Shared.DTO.Team.Request.General;
using Shared.DTO.Team.Request.PlayerRequest;
using Shared.DTO.Team.Request.TeamRequests;

namespace Application.Abstractions.Repositories;

public interface ITeamServices:ITranscientService
{
    Task<TeamModel> LoginWithEmailAndPassword(string email, string password);
    Task<TeamModel> AddNewTeam(TeamInputModel team);
    Task<List<TeamModel>> GetAllTeams();
    /// <summary>
    /// Checks if team is already registered or not
    /// </summary>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <returns>1 if phone number exist,2 if FA number exist and 0 if none exist</returns>
    Task<int> CheckTeamEmailExist(string email,string phoneNumber);
    Task<ImageRequest> TeamImageRequest(ImageRequest request);
    Task<ClubPartnersModel> AddClubPartner(ClubPartnersModel clubPartner);
    Task<List<ClubPartnersModel>> GetAllClubPartners(long teamId);
    Task<ClubKitsModel> AddClubKitModel(ClubKitsModel clubKitsModel);
    Task<List<ClubKitsModel>> GetAllKits(long teamId);
    Task<List<Player>> GetAllPlayers(long teamId);
    Task<SocialMediaHandlers> AddSocialMediaAccounts(SocialMediaHandlers media);
}