namespace Shared.DTO.Team.Request.TeamRequests;

public class TeamImageRequest
{
    public Guid ImageId { get; set; }
    public long TeamId { get; set; }
    public string ImageBlob { get; set; }
    public string ImageUrl { get; set; }
}