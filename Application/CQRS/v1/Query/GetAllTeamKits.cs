using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Query;

public class GetAllTeamKits:IRequest<List<ClubKitsModel>>
{
}