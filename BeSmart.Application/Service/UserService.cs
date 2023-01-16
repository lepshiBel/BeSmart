using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
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

        public async Task<User> FindUserByNameAsync(UserLoginRequestDTO userDto)
        {
            return await repoManager.User.GetUserByNameAsync(userDto.Username, userDto.Password);
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            return await repoManager.User.UpdateAsync(id, user);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            return await repoManager.User.DeleteAsync(id);
        }
    }

}
