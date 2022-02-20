namespace Domain.Models.Stadium;

public class StadiumModel
{
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Capacity { get; set; }
    public DateOnly BuiltDate { get; set; }
    public string Location { get; set; }
}