namespace Domain.Models.Team;

public class ClubPartnersModel
{
    public Guid ClubPartnersId { get; set; }
    public long TeamId { get; set; }
    public string BrandName { get; set; }
    public string Image { get; set; }
    public int Duration { get; set; }
    public DateOnly DateSigned { get; set; }
}