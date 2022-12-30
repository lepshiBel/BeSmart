using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceAnswer
    {
        Task<List<Answer>> GetAllAnswersAsync();
        Task<Answer> FindAnswerByIdAsync(int id);
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<Answer> UpdateAnswerAsync(Answer answer);
        Task<Answer> DeleteAnswerAsync(int id);
    }
}
