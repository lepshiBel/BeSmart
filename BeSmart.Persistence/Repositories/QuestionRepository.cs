using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class QuestionRepository : RepositoryBase<Question, DataContext>, IQuestionRepository
    {
        public QuestionRepository(DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}