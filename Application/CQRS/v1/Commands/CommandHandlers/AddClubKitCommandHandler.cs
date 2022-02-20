using Application.Abstractions.Repositories;
using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Commands.CommandHandlers;

public class AddClubKitCommandHandler:IRequestHandler<AddClubKitCommand,ClubKitsModel>
{
    private readonly ITeamServices _repo;

    public AddClubKitCommandHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<ClubKitsModel> Handle(AddClubKitCommand request, CancellationToken cancellationToken)
    {
        return await _repo.AddClubKitModel(request.ClubKit);
    }
}