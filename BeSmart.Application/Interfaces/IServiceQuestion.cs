using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceQuestion
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> FindQuestionByIdAsync(int id);
        Task<Question> AddQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(Question question);
        Task<Question> DeleteQuestionAsync(int id);
    }
}
