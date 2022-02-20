using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Query;

public class GetAllTeamsQuery:IRequest<List<TeamModel>>
{
}