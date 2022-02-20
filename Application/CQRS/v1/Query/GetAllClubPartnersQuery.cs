using Domain.Models.Team;
using MediatR;

namespace TeamManagement.Services.v1.Query;

public class GetAllClubPartnersQuery:IRequest<List<ClubKitsModel>>
{
    public long TeamId { get; set; }
}