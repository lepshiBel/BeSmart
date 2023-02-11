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
        private ITestRepository test;
        private ICardRepository card;
        private ILessonRepository lesson;
        private ICourseRepository course;
        private IThemeRepository theme;
        private IUserRepository user;
        private IMembershipRepository membership;
        private IStatusThemeRepository statusTheme;
        private IStatusLessonRepository statusLesson;
        private IStatusTestRepository statusTest;

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

        public ITestRepository Test
        {
            get
            {
                if (test == null)
                {
                    test = new TestRepository(dbContext);
                }

                return test;
            }
        }

        public ICardRepository Card
        {
            get
            {
                if (card == null)
                {
                    card = new CardRepository(dbContext);
                }

                return card;
            }
        }

        public ILessonRepository Lesson
        {
            get
            {
                if (lesson == null)
                {
                    lesson = new LessonRepository(dbContext);
                }

                return lesson;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (course == null)
                {
                    course = new CourseRepository(dbContext);
                }

                return course;
            }
        }

        public IThemeRepository Theme
        {
            get
            {
                if (theme == null)
                {
                    theme = new ThemeRepository(dbContext);
                }

                return theme;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (user == null)
                {
                    user = new UserRepository(dbContext);
                }

                return user;
            }
        }

        public IMembershipRepository Membership
        {
            get
            {
                if (membership == null)
                {
                    membership = new MembershipRepository(dbContext);
                }

                return membership;
            }
        }

        public IStatusThemeRepository StatusTheme
        {
            get
            {
                if (statusTheme == null)
                {
                    statusTheme = new ThemeStatusRepository(dbContext);
                }

                return statusTheme;
            }
        }

        public IStatusLessonRepository StatusLesson
        {
            get
            {
                if (statusLesson == null)
                {
                    statusLesson = new LessonStatusRepository(dbContext);
                }

                return statusLesson;
            }
        }

        public IStatusTestRepository StatusTest
        {
            get
            {
                if (statusTest == null)
                {
                    statusTest = new TestStatusRepository(dbContext);
                }

                return statusTest;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
