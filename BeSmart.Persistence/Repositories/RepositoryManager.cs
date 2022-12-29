using BeSmart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private BeSmartDbContext dbContext;
        private IQuestionRepository question;
        private IAnswerRepository answer;

        public RepositoryManager(BeSmartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQuestionRepository Question
        {
            get
            {
                if (question == null)
                {
                    question = new QuestionRepository(dbContext);
                }
                return question;
            }
        }

        public IAnswerRepository Answer
        {
            get
            {
                if (answer == null)
                {
                    answer = new AnswerRepository(dbContext);
                }
                return answer;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
