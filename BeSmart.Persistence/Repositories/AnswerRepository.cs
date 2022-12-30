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
    }
}