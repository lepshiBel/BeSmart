using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class QuestionService : IServiceQuestion
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public QuestionService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsAsync()
        {
            var questions = await repoManager.Question.GetAllAsync();
            return questions == null ? null : mapper.Map<List<QuestionDTO>>(questions);
        }

        public async Task<QuestionDTO> FindQuestionByIdAsync(int id)
        {
            var question = await repoManager.Question.GetAsync(id);
            return question == null ? null : mapper.Map<QuestionDTO>(question);
        }
        public async Task<QuestionWithAnswersDTO> GetQuestionWithAnswersAsync(int id)
        {
            var questionsWithAnswers = await repoManager.Question.GetQuestionWithAnswersAsync(id);
            return questionsWithAnswers == null ? null : mapper.Map<QuestionWithAnswersDTO>(questionsWithAnswers);
        }

        public async Task<QuestionDTO> AddQuestionAsync(QuestionCreationDTO questionDto)
        {
            var questionToCreate = mapper.Map<Question>(questionDto);
            var createdAnswer = await repoManager.Question.AddAsync(questionToCreate);
            return mapper.Map<QuestionDTO>(createdAnswer);
        }

        public async Task<QuestionDTO> UpdateQuestionAsync(int id, QuestionUpdateDTO questionUpdateDto)
        {
            var questionToUpdate = mapper.Map<Question>(questionUpdateDto);
            var updated = await repoManager.Question.UpdateAsync(id, questionToUpdate);
            return updated == null ? null : mapper.Map<QuestionDTO>(updated);
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            return await repoManager.Question.DeleteAsync(id);
        }
    }
}
