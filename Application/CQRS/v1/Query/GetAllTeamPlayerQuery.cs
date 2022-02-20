using Domain.Models.PlayerModels;
using MediatR;

namespace TeamManagement.Services.v1.Query;

public class GetAllTeamPlayerQuery:IRequest<List<Player>>
{
    public long TeamId { get; set; }
}