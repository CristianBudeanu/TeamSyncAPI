using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TeamSync.Application.Services.Authentification;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController(
        IAuthService authService
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetLogin(string pass)
        {
            return Ok(await authService.LoginTask(pass));
        }

        [HttpGet]
        public async Task<IActionResult> VerifyUser(string pass, string hash, string salt)
        {
            return Ok(await authService.VerifyUserTask(pass,hash,salt));
        }
    }
}
