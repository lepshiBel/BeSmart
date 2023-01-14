namespace BeSmart.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        ICategoryRepository Category { get; }
        ITestRepository Test { get; }
        ICardRepository Card { get; }
        ILessonRepository Lesson { get; }
        ICourseRepository Course { get; }
        IThemeRepository Theme { get; }
        IUserRepository User { get; }
        void Save();
    }
}
