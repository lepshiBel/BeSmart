using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public UserService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
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
            return await repoManager.User.GetUserByNameAsync(userDto.Username, userDto.Password, userDto.Email);
        }

        public async Task<User> UpdateUserByAdminAsync(int id, User user)
        {
            return await repoManager.User.UpdateAsync(id, user);
        }

        public async Task<User> UpdateUserByUserAsync(int id, UserLoginRequestDTO userLoginRequestDto)
        {
            var userToUpdate = mapper.Map<User>(userLoginRequestDto);
            var updated = await repoManager.User.UpdateAsync(id, userToUpdate);
            return updated == null ? null : mapper.Map<User>(updated);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            return await repoManager.User.DeleteAsync(id);
        }
    }

}
