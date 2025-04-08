using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Application.Dto.AuthDto;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Helpers.Authentification;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.Authentification
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly TeamSyncAppContext _context;

        public AuthService(
            IConfiguration config,
            TeamSyncAppContext context
            )
        {
            _config = config;
            _context = context;
        }

        public async Task<string> LoginTask(LoginDto dto)
        {
            var userDb = await _context.Users.Where(u => u.Username == dto.Username).Include(r => r.Role).FirstOrDefaultAsync();

            if (userDb == null)
            {
                throw new NotFoundException("User not found!");
            }

            if (PassHasher.VerifyPassword(dto.Password, userDb.PassHash, userDb.PassSalt))
            {
                var token = GenerateJWT(userDb);
                return token;
            }

            throw new BadRequestException("Incorrect Username/Password!");
        }

        public async Task<bool> RegisterTask(RegisterDto dto)
        {
            var existUser = await _context.Users.Where(u => u.Username == dto.Username).FirstOrDefaultAsync();

            if (existUser == null)
            {
                var (hash, salt) = PassHasher.HashPassword(dto.Password);
                var userData = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PassHash = hash,
                    PassSalt = salt,
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                };

                await _context.Users.AddAsync(userData);
                int result = await _context.SaveChangesAsync();

                return result > 0 ? true : throw new BadRequestException("Error register user.");
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
                new Claim("Role", $"{user.Role.RoleName}"),
            };

            var token = new JwtSecurityToken(
                claims: identity,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
