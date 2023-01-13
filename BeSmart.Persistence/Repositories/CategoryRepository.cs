using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, BeSmartDbContext>, ICategoryRepository
    {
        public CategoryRepository(BeSmartDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
