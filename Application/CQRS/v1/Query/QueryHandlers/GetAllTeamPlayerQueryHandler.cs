using Application.Abstractions.Repositories;
using Domain.Models.PlayerModels;
using MediatR;

namespace TeamManagement.Services.v1.Query.QueryHandlers;

public class GetAllTeamPlayerQueryHandler:IRequestHandler<GetAllTeamPlayerQuery,List<Player>>
{
    private readonly ITeamServices _repo;
    public GetAllTeamPlayerQueryHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<List<Player>> Handle(GetAllTeamPlayerQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllPlayers(request.TeamId);
    }
}