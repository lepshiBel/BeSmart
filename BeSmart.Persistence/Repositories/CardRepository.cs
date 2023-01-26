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

        public override async Task<Card> UpdateAsync(int id, Card card)
        {
            var old = context.Cards.FirstOrDefault(c => c.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Word = card.Word;
            old.Text = card.Text;
            old.ImageUrl = card.ImageUrl;
            old.Transctipt = card.Transctipt;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}
