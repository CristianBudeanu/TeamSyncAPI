using Mapster;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Dto.AuthDto;
using TeamSync.Application.Services.Authentification;
using TeamSync.Web.Models.AuthModels;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(
        IAuthService authService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel data)
        {
            var result = await authService.LoginTask(data.Adapt<LoginDto>());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel data)
        {
            var result = await authService.RegisterTask(data.Adapt<RegisterDto>());
            return Ok(result);
        }
    }
}
