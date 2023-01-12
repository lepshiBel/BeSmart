using BeSmart.Server.Application.Interfaces;
using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repoManager;

        public UserService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await repoManager.User.GetAllAsync();
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            return await repoManager.User.GetAsync(id);
        }
    }
}
