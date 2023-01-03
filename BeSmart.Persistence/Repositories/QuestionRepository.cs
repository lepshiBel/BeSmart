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
    }
}