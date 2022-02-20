using Domain.Models.PlayerModels;

namespace Domain.Models.Team;

public class TeamModel
{
    public string ClubName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Slogan { get; set; }
    public string Website { get; set; }
    public string ImageId { get; set; }
    public int DivisionId { get; set; }
    public int StadiumId { get; set; }
    public string TeamCode { get; set; }
    public List<Player> TeamSquads { get; set; }
    public List<FixtureModel> Fixtures { get; set; }
    public List<ResultModel> Results { get; set; }
    public int StatisticsId { get; set; }
    public int TicketsId { get; set; }
    public int SeasonHistoryId { get; set; }
    public List<ClubPartnersModel> ClubPartners { get; set; }
    public List<ClubKitsModel> Kits { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
}


