using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSync.Application.Dto;
using TeamSync.Application.Dto.AuthDto;

namespace TeamSync.Application.Services.Authentification
{
    public interface IAuthService
    {
        public Task<string> LoginTask(LoginDto user);
        public Task<bool> RegisterTask(RegisterDto user);
    }
}
