using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Service
{
    public class QuestionService : IServiceQuestion
    {
        private readonly IRepositoryManager repoManager;

        public QuestionService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await repoManager.Question.GetAllAsync();
        }

        public async Task<Question> FindQuestionByIdAsync(int id)
        {
            return await repoManager.Question.GetAsync(id);
        }
        public async Task<Question> GetQuestionWithAnswersAsync(int id)
        {
            return await repoManager.Question.GetQuestionWithAnswersAsync(id);
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            return await repoManager.Question.AddAsync(question);
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {

            return await repoManager.Question.UpdateAsync(question); 
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            return await repoManager.Question.DeleteAsync(id);
        }
    }
}
