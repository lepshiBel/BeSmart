namespace BeSmart.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        ICategoryRepository Category { get; }
        ITestRepository Test { get; }
        ICardRepository Card { get; }
        void Save();
    }
}
