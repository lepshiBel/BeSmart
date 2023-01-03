using BeSmart.Domain.Interfaces;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private BeSmartDbContext dbContext;

        private IQuestionRepository question;
        private IAnswerRepository answer;
        private ICategoryRepository category;

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
        public ICategoryRepository Category
        {
            get
            {
                if (category == null)
                {
                    category = new CategoryRepository(dbContext);
                }
                return category;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
