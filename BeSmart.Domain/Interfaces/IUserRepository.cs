using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByNameAsync(string username, string email);
    }
}
