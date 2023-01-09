using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Repositories
{
    public class QuestionRepository : RepositoryBase<Question, BeSmartDbContext>, IQuestionRepository
    {
        public QuestionRepository(BeSmartDbContext dbContext) : base(dbContext) { }

        public async Task<Question> GetQuestionWithAnswersAsync(int id)
        {
            var question = await context.Questions.FindAsync(id);
            await context.Entry(question).Collection(q => q.Answers).LoadAsync();
            return question;
        }
        public override async Task<Question> UpdateAsync(int id, Question answer)
        {
            var old = context.Questions.FirstOrDefault(o => o.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Text = answer.Text;
            old.TestId = answer.TestId;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}