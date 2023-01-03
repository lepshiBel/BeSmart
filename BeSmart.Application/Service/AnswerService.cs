using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Service
{
    public class AnswerService : IServiceAnswer
    {
        private readonly IRepositoryManager repoManager;

        public AnswerService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<Answer>> GetAllAnswersAsync()
        {
            return await repoManager.Answer.GetAllAsync();
        }

        public async Task<Answer> FindAnswerByIdAsync(int id)
        {
            return await repoManager.Answer.GetAsync(id);
        }

        public async Task<Answer> AddAnswerAsync(Answer entity)
        {
             return await repoManager.Answer.AddAsync(entity);
        }

        public async Task<Answer> UpdateAnswerAsync(Answer entity)
        {
            return await repoManager.Answer.UpdateAsync(entity);
        }

        public async Task<Answer> DeleteAnswerAsync(int id)
        {
            return await repoManager.Answer.DeleteAsync(id);
        }
    }
}
