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
        private readonly ICacheService _cacheService;

        public QuestionService(IRepositoryManager repoManager, ICacheService cacheService, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(QuestionService), dataId);
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<QuestionDTO>>(cacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }

            var questions = await repoManager.Question.GetAllAsync();
            var mappedQuestions = questions == null ? null : mapper.Map<List<QuestionDTO>>(questions);

            if (mappedQuestions is not null)
            {
                await _cacheService.CacheDataAsync(cacheKey, mappedQuestions);
            }

            return mappedQuestions;
        }

        public async Task<QuestionDTO> FindQuestionByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<QuestionDTO>(cacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }

            var question = await repoManager.Question.GetAsync(id);
            var mappedQuestion = question == null ? null : mapper.Map<QuestionDTO>(question);

            return mappedQuestion is not null ? await _cacheService.CacheDataAsync(cacheKey, mappedQuestion)
                : mappedQuestion;
        }

        public async Task<QuestionWithAnswersDTO> GetQuestionWithAnswersAsync(int id)
        {
            var cacheKey = GetCacheKey($"with_answers-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<QuestionWithAnswersDTO>(cacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }
            
            var questionsWithAnswers = await repoManager.Question.GetQuestionWithAnswersAsync(id);
            var mappedResult = questionsWithAnswers == null ? null : mapper.Map<QuestionWithAnswersDTO>(questionsWithAnswers);

            return mappedResult is not null ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<QuestionDTO> AddQuestionAsync(QuestionCreationDTO questionDto)
        {
            var questionToCreate = mapper.Map<Question>(questionDto);
            var createdAnswer = await repoManager.Question.AddAsync(questionToCreate);
            return mapper.Map<QuestionDTO>(createdAnswer);
        }

        public async Task<QuestionDTO> UpdateQuestionAsync(int id, QuestionCreationDTO questionUpdateDto)
        {
            var questionToUpdate = mapper.Map<Question>(questionUpdateDto);
            var updated = await repoManager.Question.UpdateAsync(id, questionToUpdate);
            var updatedMappedQuestion = updated == null ? null : mapper.Map<QuestionDTO>(updated);

            if (updatedMappedQuestion is not null)
            {
                var cacheKey = _cacheService.GetKey(nameof(QuestionService), id.ToString());
                await _cacheService.CacheDataAsync(cacheKey, updatedMappedQuestion);
            }

            return updatedMappedQuestion;
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            var deletedQuestion = await repoManager.Question.DeleteAsync(id);

            if (deletedQuestion is not null)
            {
                var cacheKey = _cacheService.GetKey(nameof(QuestionService), id.ToString());
                await _cacheService.DeleteCachedData(cacheKey);
            }

            return deletedQuestion;
        }
    }
}
