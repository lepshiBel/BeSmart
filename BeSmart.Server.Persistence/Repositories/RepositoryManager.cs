using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Persistence.Data;

namespace BeSmart.Server.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private BeSmartDbContext dbContext;
        private IUserRepository user;

        public RepositoryManager(BeSmartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUserRepository User
        {
            get
            {
                if (user == null)
                {
                    user = new UserRepository(dbContext);
                }
                return user;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
