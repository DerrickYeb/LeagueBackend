namespace Domain.Models;

public class ResultModel
{
    public long TeamId { get; set; }
    public DateOnly PlayedDate { get; set; }
    public DateTime PlayedTime { get; set; }
    public string Scoreline { get; set; }
}