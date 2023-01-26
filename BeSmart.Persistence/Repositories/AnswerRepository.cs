using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer, BeSmartDbContext>, IAnswerRepository
    {
        public AnswerRepository(BeSmartDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<Answer> UpdateAsync(int id, Answer answer)
        {
            var old = context.Answers.FirstOrDefault(o => o.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Text = answer.Text;
            old.Fidelity = answer.Fidelity;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}