using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class QuestionRepository : RepositoryBase<Question, BeSmartDbContext>, IQuestionRepository
    {
        public QuestionRepository(BeSmartDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}