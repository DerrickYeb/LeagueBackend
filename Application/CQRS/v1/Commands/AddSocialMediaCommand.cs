using MediatR;
using Shared.DTO.Team.Request.General;

namespace TeamManagement.Services.v1.Commands;

public class AddSocialMediaCommand:IRequest<SocialMediaHandlers>
{
    public SocialMediaHandlers SocialAccounts { get; set; }
}