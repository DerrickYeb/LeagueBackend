using Application.Abstractions.Repositories;
using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Commands.CommandHandlers;

public class AddClubPartnerCommandHandler:IRequestHandler<AddClubPartnerCommand,ClubPartnersModel>
{
    private readonly ITeamServices _repo;

    public AddClubPartnerCommandHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<ClubPartnersModel> Handle(AddClubPartnerCommand request, CancellationToken cancellationToken)
    {
        return await _repo.AddClubPartner(request.ClubPartner);
    }
}