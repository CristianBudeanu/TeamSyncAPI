using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSync.Application.Services.Authentification
{
    public interface IAuthService
    {
        public Task<string> LoginTask(string password);
        public Task<bool> VerifyUserTask(string password, string hash, string salt);
    }
}
