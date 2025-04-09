using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Services.Chat;
using TeamSync.Web.Models.ChatModels;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController(IChatService _chatService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(ChatUserDto model)
        {
            return NoContent ();
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectMessages(Guid projectId)
        {
            return Ok(await _chatService.GetProjectMessagesTask(projectId));
        }

    }
}
