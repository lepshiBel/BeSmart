using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IQuestionRepository : IRepositoryBase<Question>
    {
        Task<Question> GetQuestionWithAnswersAsync(int id);
    }
}