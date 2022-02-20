using Domain.Enums;

namespace Domain.Models.Team;

public class ClubKitsModel
{
    public long TeamId { get; set; }
    public string BrandName { get; set; }
    public KitType KitType { get; set; }
    public List<string> Colors { get; set; }
}