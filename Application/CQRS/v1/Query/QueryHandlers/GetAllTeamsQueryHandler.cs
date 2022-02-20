using Application.Abstractions.Repositories;
using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Query.QueryHandlers;

public class GetAllTeamsQueryHandler:IRequestHandler<GetAllTeamsQuery,List<TeamModel>>
{
    private readonly ITeamServices _repo;
    public GetAllTeamsQueryHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<List<TeamModel>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllTeams();
    }
}