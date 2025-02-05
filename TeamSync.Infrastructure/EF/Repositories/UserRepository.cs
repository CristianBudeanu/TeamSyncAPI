using Microsoft.EntityFrameworkCore;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Infrastructure.EF.Repositories
{
    public class UserRepository: IUserRepository
    {

        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);

                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserAsync(string username)
        {
            try
            {
               return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> RemoveUserAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
