using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> FindUserByIdAsync(int id);
    }
}
