using Domain.Models.PlayerModels;
using MediatR;
using Shared.DTO.Team.Request.PlayerRequest;

namespace TeamManagement.Services.v1.Commands;

public class CreatePlayerCommand:IRequest<Player>
{
    public PlayerInputModel PlayerRequest { get; set; }
}