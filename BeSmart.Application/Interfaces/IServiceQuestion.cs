using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceQuestion
    {
        Task<List<QuestionDTO>> GetAllQuestionsAsync();
        Task<QuestionDTO> FindQuestionByIdAsync(int id);
        Task<QuestionWithAnswersDTO> GetQuestionWithAnswersAsync(int id);
        Task<QuestionDTO> AddQuestionAsync(QuestionCreationDTO questionDto);
        Task<QuestionDTO> UpdateQuestionAsync(int id, QuestionCreationDTO question);
        Task<Question> DeleteQuestionAsync(int id);
    }
}
