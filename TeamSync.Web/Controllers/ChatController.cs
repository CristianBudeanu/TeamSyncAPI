using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Services.Chat;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController(ChatService _chatService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser(string username)
        {
            if (_chatService.AddUserToList(username))
            {
                return NoContent();
            }

            return BadRequest("This name is taken please choose another name.");
        }
    }
}
