using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> FindUserByIdAsync(int id);
        Task<User> FindUserByNameAsync(UserLoginRequestDTO userDto);
    }
}
