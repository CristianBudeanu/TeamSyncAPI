using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Services.ProjectServices.InvitationServices;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvitationController(IInvitationService _invitationService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateInvitationLink(Guid projectId)
        {
            var invitationToken = await _invitationService.CreateProjectInvitationTask(projectId);
            return Ok(invitationToken);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AcceptInvitationLink(Guid invitationToken)
        {
            var response = await _invitationService.AcceptProjectInvitationTask(invitationToken);
            return Ok(response);
        }
    }
}
