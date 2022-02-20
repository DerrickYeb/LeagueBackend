using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Commands;

public class AddClubPartnerCommand:IRequest<ClubPartnersModel>
{
    public ClubPartnersModel ClubPartner { get; set; }
}