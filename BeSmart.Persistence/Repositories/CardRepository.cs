using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class CardRepository : RepositoryBase<Card, BeSmartDbContext>, ICardRepository
    {
        public CardRepository(BeSmartDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
