namespace Shared.DTO.Team.Request.TeamRequests;

public class TeamLogin
{
    public Guid TeamLoginId { get; set; }
    public long TeamId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}