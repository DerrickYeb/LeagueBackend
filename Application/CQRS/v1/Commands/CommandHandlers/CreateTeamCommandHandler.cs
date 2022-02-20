using System.ComponentModel;
using Application.Abstractions.Repositories;
using Domain.Models.Team;
using MediatR;
using Shared.DTO.Team.Request.TeamRequests;

namespace TeamManagement.Services.v1.Commands.CommandHandlers;

public class CreateTeamCommandHandler:IRequestHandler<CreateTeamCommand,TeamModel>
{
    private readonly ITeamServices _team;
    public CreateTeamCommandHandler(ITeamServices team)
    {
        _team = team;
    }
    public async Task<TeamModel> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
       return await (_team.AddNewTeam(request.Team));
    }
}