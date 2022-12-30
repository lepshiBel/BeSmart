namespace BeSmart.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        void Save();
    }
}
