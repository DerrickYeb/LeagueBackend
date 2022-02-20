using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Commands;

public class AddClubKitCommand : IRequest<ClubKitsModel>
{
    public ClubKitsModel ClubKit { get; set; }
}