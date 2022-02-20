using System.Data;
using System.Drawing;
using Application.Abstractions.DataHelper;
using Application.Abstractions.Repositories;
using Application.Extensions;
using Dapper;
using Domain.Models;
using Domain.Models.PlayerModels;
using Domain.Models.Team;
using Microsoft.Extensions.Logging;
using Shared.DTO.Team.Request;
using Shared.DTO.Team.Request.General;
using Shared.DTO.Team.Request.PlayerRequest;
using Shared.DTO.Team.Request.TeamRequests;

namespace Application.Services.Team;

public class TeamServices : ITeamServices
{
    private readonly IDbConnector _connector;
    private readonly ILogger<TeamServices> _logger;

    public TeamServices(ILogger<TeamServices> logger, IDbConnector dbConnector)
    {
        _logger = logger;
        _connector = dbConnector;
    }

    public async Task<TeamModel> LoginWithEmailAndPassword(string email, string password)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<TeamModel>(
                $"{_connector.GetTeamSchema()}.login_with_email_and_password",
                new
                {
                    _email = email,
                    _password = password
                }, commandType: CommandType.StoredProcedure);
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
                return result;
            return null!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TeamModel> AddNewTeam(TeamInputModel team)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<TeamModel>($"{_connector.GetTeamSchema()}.add_new_team",
                new
                {
                    _team_code = team.TeamCode.GenerateTeamInitialsWithCode(),
                    _team_name = team.ClubName,
                    _slogan = team.Slogan,
                    _email = team.Email,
                    _website = team.Website,
                    _password = team.Password.PasswordHashEncrypt(),
                    _division_type_id = team.DivisionTypeId,
                    _short_hand = team.ShortHand,
                    _phone_number = team.PhoneNumber,
                    _created_date = team.CreatedDate
                }, commandType: CommandType.StoredProcedure);
            return result ?? null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(AddNewTeam), new { team }, e.StackTrace);
            return null;
        }
    }

    public async Task<List<TeamModel>?> GetAllTeams()
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QueryAsync<TeamModel>($"{_connector.GetTeamSchema()}.get_all_teams",
                commandType: CommandType.StoredProcedure);
            return result?.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Traces}\n",
                nameof(GetAllTeams), e.StackTrace);
            return null;
        }
    }

    public async Task<int> CheckPlayerExist(string phoneNumber, string footballAssociationNumber)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<int>($"{_connector.CreateConnection()}.check_player_exists",
                new
                {
                    _phone_number = phoneNumber,
                    _foot_ball_association_number = footballAssociationNumber
                }, commandType: CommandType.StoredProcedure);
            if (!string.IsNullOrWhiteSpace(phoneNumber) && !string.IsNullOrWhiteSpace(footballAssociationNumber))
                return result;
            return 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error while executing {MethodName}-{Input} {Traces}\n",
                nameof(CheckPlayerExist), new { phoneNumber, footballAssociationNumber }, e.StackTrace);
            return 0;
        }
    }

    public async Task<int> CheckTeamEmailExist(string email, string phoneNumber)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<int>(
                $"{_connector.GetTeamSchema()}.check_team_email_phone_exist",
                new
                {
                    _email = email,
                    _phone_number = phoneNumber
                }, commandType: CommandType.StoredProcedure);
            if (!string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
                return result;
            return 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(CheckTeamEmailExist), new { email, phoneNumber }, e.StackTrace);
            return 0;
        }
    }

    public async Task<ImageRequest> TeamImageRequest(ImageRequest request)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<ImageRequest>(
                $"{_connector.GetTeamSchema()}.add_image",
                new
                {
                    _team_id = request.TeamId,
                    _image_blob = request.ImageBlob,
                    _image_url = request.ImageUrl
                }, commandType: CommandType.StoredProcedure);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(TeamImageRequest), new { request }, e.StackTrace);
            return null;
        }
    }

    public async Task<ClubPartnersModel> AddClubPartner(ClubPartnersModel clubPartner)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<ClubPartnersModel>(
                $"{_connector.GetTeamSchema()}.add_club_partner",
                new
                {
                    _team_id = clubPartner.TeamId,
                    _brand_name = clubPartner.BrandName,
                    _duration = clubPartner.Duration,
                    _date_signed = clubPartner.DateSigned
                }, commandType: CommandType.StoredProcedure);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(AddClubPartner), new { clubPartner }, e.StackTrace);
            return null;
        }
    }


    public async Task<Player> AddTeamPlayer(PlayerInputModel player)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<Player>(
                $"{_connector.GetHistorySchema()}.add_team_player",
                new
                {
                    _team_id = player.TeamId,
                    _player_code = player.PlayerCode,
                    _first_name = player.FirstName,
                    _last_name = player.LastName,
                    _other_name = player.OtherName,
                    _email = player.Email,
                    _password = player.Password.PasswordHashEncrypt(),
                    _phone_number = player.PhoneNumber,
                    _dob = player.Dob,
                    _preferred_foot = player.PreferedFoot,
                    _home_town = player.HomeTown,
                    _short_hand = player.ShortHand,
                    _id_type_id = player.IdTypeId,
                    _id_type_number = player.IdTypeNumber,
                    _position_id = player.PosistionId,
                    _kit_number = player.KitNumber,
                    _nick_name = player.NickName,
                    _country = player.Country,
                    _birth_certificate_number = player.BirthCertificate
                }, commandType: CommandType.StoredProcedure);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(AddTeamPlayer), new { player }, e.StackTrace);
            return null;
        }
    }

    public async Task<List<FixtureModel>> GetAllFixtures(long teamId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ClubPartnersModel>> GetAllClubPartners(long teamId)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QueryAsync<ClubPartnersModel>(
                $"{_connector.GetTeamSchema()}.get_all_team_partners",
                new { _team_id = teamId },
                commandType: CommandType.StoredProcedure);
            var clubPartnersModels = result.ToList();
            if (clubPartnersModels.Any())
                return clubPartnersModels.ToList();
            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(GetAllClubPartners), new { teamId }, e.StackTrace);
            return null;
        }
    }

    public async Task<ClubKitsModel> AddClubKitModel(ClubKitsModel clubKitsModel)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<ClubKitsModel>(
                $"{_connector.GetTeamSchema()}.add_team_kit",
                new
                {
                    _team_id = clubKitsModel.TeamId,
                    _colors = clubKitsModel.Colors,
                    _kit_type = clubKitsModel.KitType
                }, commandType: CommandType.StoredProcedure);
            return result ?? null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(AddClubKitModel), new { clubKitsModel }, e.StackTrace);
            return null;
        }
    }

    public async Task<List<ClubKitsModel>> GetAllKits(long teamId)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QueryAsync<ClubKitsModel>($"{_connector.GetTeamSchema()}.get_all_kits",
                new
                {
                    _team_id = teamId
                }, commandType: CommandType.StoredProcedure);
            return result?.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(GetAllKits), new { teamId }, e.StackTrace);
            return null;
        }
    }

    public async Task<List<Player>> GetAllPlayers(long teamId)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QueryAsync<Player>($"{_connector.GetHistorySchema()}.get_all_players",
                new
                {
                    _team_Id = teamId
                }, commandType: CommandType.StoredProcedure);
            return result?.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(GetAllPlayers), new { teamId }, e.StackTrace);
            return null;
        }
    }

    public async Task<SocialMediaHandlers> AddSocialMediaAccounts(SocialMediaHandlers media)
    {
        try
        {
            var connection = _connector.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<SocialMediaHandlers>(
                $"{_connector.GetTeamSchema()}.add_social_media_accounts",
                new
                {
                    _social_media_handlers_id = media.SocialMediaHandlersId,
                    _team_id = media.TeamId,
                    _account_type = media.AccountType,
                    _handler_url = media.HandlerUrl,
                    _created_date = media.CreatedDate
                }, commandType: CommandType.StoredProcedure);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while executing {MethodName}-{Input} {Traces}\n",
                nameof(AddSocialMediaAccounts), new { media }, e.StackTrace);
            return null;
        }
    }
}