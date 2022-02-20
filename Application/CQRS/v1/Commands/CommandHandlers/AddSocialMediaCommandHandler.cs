using Application.Abstractions.Repositories;
using MediatR;
using Shared.DTO.Team.Request.General;

namespace TeamManagement.Services.v1.Commands.CommandHandlers;

public class AddSocialMediaCommandHandler:IRequestHandler<AddSocialMediaCommand,SocialMediaHandlers>
{
    private readonly ITeamServices _repo;

    public AddSocialMediaCommandHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    public async Task<SocialMediaHandlers> Handle(AddSocialMediaCommand request, CancellationToken cancellationToken)
    {
        return await _repo.AddSocialMediaAccounts(request.SocialAccounts);
    }
}