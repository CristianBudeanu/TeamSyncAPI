using TeamSync.Application.Dto.AuthDto;

namespace TeamSync.Application.Services.Authentification
{
    public interface IAuthService
    {
        public Task<string> LoginTask(LoginDto dto);
        public Task<bool> RegisterTask(RegisterDto dto);
    }
}
