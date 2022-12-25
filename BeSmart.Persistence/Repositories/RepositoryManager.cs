using BeSmart.Domain.Interfaces;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext dbContext;
        private IQuestionRepository question;
        private IAnswerRepository answer;

        public RepositoryManager(DataContext dbContext)
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
