namespace Domain.Models;

public class FixtureModel
{
    public long TeamId { get; set; }
    public string Team { get; set; }
    public DateTime GameTime { get; set; }
    public DateOnly GameDay { get; set; }
    public string Venue { get; set; }
    public string ChannelId { get; set; }
}