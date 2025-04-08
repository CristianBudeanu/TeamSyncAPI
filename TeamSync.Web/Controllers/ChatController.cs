using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Services.Chat;
using TeamSync.Web.Models.ChatModels;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController(ChatService _chatService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(ChatUserDto model)
        {
            if (_chatService.AddUserToList(model.Username))
            {
                return NoContent();
            }

            return BadRequest("This name is taken please choose another name.");
        }
    }
}
