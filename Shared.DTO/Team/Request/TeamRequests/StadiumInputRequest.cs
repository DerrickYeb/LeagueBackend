namespace Shared.DTO.Team.Request.TeamRequests;

public class StadiumInputRequest
{
    public int StadiumId { get; set; }
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Capacity { get; set; }
    public DateOnly BuiltDate { get; set; }
    public string Location { get; set; }
    public DateOnly CreatedDate { get; set; }
}