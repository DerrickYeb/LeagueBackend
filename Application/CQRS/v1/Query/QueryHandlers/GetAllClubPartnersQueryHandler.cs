using Application.Abstractions.Repositories;
using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Query.QueryHandlers;

public class GetAllClubPartnersQueryHandler:IRequestHandler<GetAllClubPartnersQuery,List<ClubKitsModel>>
{
    private readonly ITeamServices _repo;
    public GetAllClubPartnersQueryHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<List<ClubKitsModel>> Handle(GetAllClubPartnersQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllKits(request.TeamId);
    }
}