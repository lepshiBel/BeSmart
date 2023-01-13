using BeSmart.Server.Domain;
using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Domain.Models;
using BeSmart.Server.Persistence.Data;

namespace BeSmart.Server.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, BeSmartDbContext>, IUserRepository
    {
        public UserRepository(BeSmartDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
