namespace Shared.DTO.Team.Request.General;

public class SocialMediaHandlers
{
    public int SocialMediaHandlersId { get; set; }
    public long? TeamId { get; set; }
    public string AccountType { get; init; }
    public string HandlerUrl { get; init; }
    public DateOnly CreatedDate { get; init; }
}