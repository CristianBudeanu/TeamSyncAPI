using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamSync.Application.Dto.AuthDto;
using TeamSync.Application.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Helpers.Authentification;
using TeamSync.Infrastructure.EF.Repositories;

namespace TeamSync.Application.Services.Authentification
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(
            IConfiguration config, 
            IUserRepository userRepository
            )
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> LoginTask(LoginDto data)
        {
            var userDb = await _userRepository.GetUserAsync(data.Username);

            if (userDb == null)
            {
                throw new NotFoundException("User not found!");
            }

            if (PassHasher.VerifyPassword(data.Password, userDb.PassHash, userDb.PassSalt))
            {
                var userData = new User()
                {
                    Username = data.Username,
                    Role = Domain.Enums.Roles.User
                };

                var token = GenerateJWT(userData);
                return token;
            }

            throw new BadRequestException("Incorrect Username/Password!");
        }

        public async Task<bool> RegisterTask(RegisterDto data)
        {
            var existUser = await _userRepository.GetUserAsync(data.Username);

            if (existUser == null) 
            {
                var (hash, salt) = PassHasher.HashPassword(data.Password);
                var userData = new User
                {
                    Username = data.Username,
                    Email = data.Email,
                    PassHash = hash,
                    PassSalt = salt,
                    Role = Domain.Enums.Roles.User
                };

                return await _userRepository.AddUserAsync(userData);
            }
            throw new BadRequestException("User with this name already exists!");
        }

        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var identity = new[]
            {
                new Claim("Username", $"{user.Username}"),
                new Claim("Role", $"{user.Role}"),
            };

            var token = new JwtSecurityToken(
                claims: identity,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
