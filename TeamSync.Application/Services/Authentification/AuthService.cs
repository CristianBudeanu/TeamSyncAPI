using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSync.Helpers.Authentification;

namespace TeamSync.Application.Services.Authentification
{
    public class AuthService : IAuthService
    {
        public async Task<string> LoginTask(string password)
        {
            var (hash, salt) = PassHasher.HashPassword(password);
            return hash + " | " + salt;
        }

        public async Task<bool> VerifyUserTask(string password, string hash, string salt)
        {
            return PassHasher.VerifyPassword(password, hash, salt);
        }
    }
}
