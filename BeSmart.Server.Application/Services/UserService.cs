using BeSmart.Server.Application.Interfaces;
using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User> {
            new User {
                Id = 1, Username = "mytestuser", Email = "somemail_user@gmail.com", Password = "12345", Role = "user"
            },
            new User {
                Id = 2, Username = "mytestadmin", Email = "somemail_admin@gmail.com", Password = "12345", Role = "admin"
            }
        };

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            return await userRepository.GetAsync(id);
        }
    }
}
