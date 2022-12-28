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
        Task<Question> AddQuestionAsync(Question entity);
        Task<Question> UpdateQuestionAsync(Question entity);
        Task<Question> DeleteQuestionAsync(int id);
    }
}
