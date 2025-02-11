using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Infrastructure.EF.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> AddUserAsync(User user);
        public Task<User> GetUserAsync(string username);
        public Task<bool> RemoveUserAsync(string username);
    }
}
