using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceQuestion
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> FindQuestionByIdAsync(int id);
        Task<Question> GetQuestionWithAnswersAsync(int id);
        Task<Question> AddQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(Question question);
        Task<Question> DeleteQuestionAsync(int id);
    }
}
