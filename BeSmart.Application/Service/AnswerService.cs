using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Service
{
    public class AnswerService : IServiceAnswer
    {
        private readonly IRepositoryManager repoManager;
        private readonly IValidator<Answer> validator;
        public AnswerService(IRepositoryManager repoManager, IValidator<Answer> validator)
        {
            this.repoManager = repoManager;
            this.validator = validator;
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
