using Domain.Models.Team;
using MediatR;
using Shared.DTO.Team.Request.TeamRequests;

namespace TeamManagement.Services.v1.Commands;

public class CreateTeamCommand:IRequest<TeamModel>
{
    public TeamInputModel Team { get; set; }
}