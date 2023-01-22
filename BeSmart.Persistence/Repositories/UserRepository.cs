using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, BeSmartDbContext>, IUserRepository
    {
        public UserRepository(BeSmartDbContext dbContext)
        : base(dbContext)
        {
        }

        public async Task<User> GetUserByNameAsync(string username) //Искать по юзернейму и почте, вместо пароля хеш
        {
            var user = context.Users.SingleOrDefault(u => u.Username == username);
            return user;
        }
    }
}
