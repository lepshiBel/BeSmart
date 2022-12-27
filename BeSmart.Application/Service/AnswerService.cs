using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
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
        public AnswerService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<Answer>> GetAllAsync()
        {
            return await repoManager.Answer.GetAllAsync();
        }

        public async Task<Answer> FindByIdAsync(int id)
        {
            return await repoManager.Answer.GetAsync(id);
        }

        public async Task<Answer> AddAsync(Answer entity)
        {
             return await repoManager.Answer.AddAsync(entity);
        }

        public async Task<Answer> UpdateAsync(Answer entity)
        {
            return await repoManager.Answer.UpdateAsync(entity);
        }

        public async Task<Answer> DeleteAsync(int id)
        {
            return await repoManager.Answer.DeleteAsync(id);
        }
    }
}
