using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceAnswer
    {
        Task<List<AnswerDTO>> GetAllAnswersAsync();
        Task<AnswerDTO> FindAnswerByIdAsync(int id);
        Task<AnswerDTO> AddAnswerAsync(AnswerCreationDTO answer);
        Task<AnswerDTO> UpdateAnswerAsync(int id, AnswerUpdateDTO answerUpdateDto);
        Task<Answer> DeleteAnswerAsync(int id);
    }
}
