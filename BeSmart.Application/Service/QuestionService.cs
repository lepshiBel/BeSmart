using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Service
{
    public class QuestionService : IServiceQuestion
    {
        private readonly IRepositoryManager repoManager;
        public QuestionService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<Question>> GetAllAsync()
        {
            return await repoManager.Question.GetAllAsync();
        }

        public async Task<Question> FindByIdAsync(int id)
        {
            return await repoManager.Question.GetAsync(id);
        }

        public async Task<Question> AddAsync(Question entity)
        {
            return await repoManager.Question.AddAsync(entity);
        }

        public async Task<Question> UpdateAsync(Question entity)
        {
            return await repoManager.Question.UpdateAsync(entity);
        }

        public async Task<Question> DeleteAsync(int id)
        {
            return await repoManager.Question.DeleteAsync(id);
        }
    }
}
