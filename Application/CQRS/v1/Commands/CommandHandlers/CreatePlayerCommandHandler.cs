using Application.Abstractions.Repositories;
using Domain.Models.PlayerModels;
using MediatR;
using TeamManagement.Services.v1.Commands;

namespace Application.CQRS.v1.Commands.CommandHandlers;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly ITeamServices _repo;
    public CreatePlayerCommandHandler(ITeamServices repo)
    {
        _repo = repo;
    }
    // public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    // {
    //     return await _repo.AddTeamPlayer(request.PlayerRequest);
    // }
    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}