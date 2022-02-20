namespace Shared.DTO.Team.Request.TeamRequests;

public class TeamInputModel
{
    public long TeamId { get; set; }
    public string ShortHand { get; set; }
    public string ClubName { get; set; }
    public string Slogan { get; set; }
    public string Website { get; set; }
    public string TeamCode { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string DivisionTypeId { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
}