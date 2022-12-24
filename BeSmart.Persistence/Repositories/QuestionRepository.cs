using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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