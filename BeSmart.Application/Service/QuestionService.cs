using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using FluentValidation;
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
        private readonly IValidator<Question> validator;

        public QuestionService(IRepositoryManager repoManager, IValidator<Question> validator)
        {
            this.repoManager = repoManager;
            this.validator = validator;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await repoManager.Question.GetAllAsync();
        }

        public async Task<Question> FindQuestionByIdAsync(int id)
        {
            return await repoManager.Question.GetAsync(id);
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            var validationResult = validator.Validate(question);
            if(validationResult.IsValid)
            {
                return await repoManager.Question.AddAsync(question);
            }
            return null;
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            var validationResult = validator.Validate(question);
            if (validationResult.IsValid)
            {
                return await repoManager.Question.UpdateAsync(question);
            }
            return null;      
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            return await repoManager.Question.DeleteAsync(id);
        }
    }
}
