using Mapster;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Dto.AuthDto;
using TeamSync.Application.Services.Authentification;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(
        IAuthService authService,
        ILogger<AuthController> logger
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto data)
        {
            var result = await authService.LoginTask(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto data)
        {
            var result = await authService.RegisterTask(data);
            return Ok(result);
        }
    }
}
