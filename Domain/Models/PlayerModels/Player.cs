using Domain.Models.Team;
using Shared.DTO.Team.Request;
using Shared.DTO.Team.Request.General;

namespace Domain.Models.PlayerModels;

public class Player
{
    public string PlayerCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OtherName { get; set; }
    public string Email { get; set; }
    public int IdTypeId { get; set; }
    public string IdTypeNumber { get; set; }
    public string BirthCertificate { get; set; }
    public DateOnly Dob { get; set; }
    public string PosistionId { get; set; }
    public string KitNumber { get; set; }
    public string NickName { get; set; }
    public string ShortHand { get; set; }
    public List<TeamModel> Clubs { get; set; }
    public List<SocialMediaHandlers> SocialMediaHandlers { get; set; }
    public string PreferedFoot { get; set; }
    public string Country { get; set; }
    public string HomeTown { get; set; }
}