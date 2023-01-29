using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class AnswerService : IServiceAnswer
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        private readonly ICacheService _cacheService;

        public AnswerService(IRepositoryManager repoManager, ICacheService cacheService, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(AnswerService), dataId);
        }
        
        public async Task<List<AnswerDTO>>? GetAllAnswersAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<AnswerDTO>>(cacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }
            
            var answers = await repoManager.Answer.GetAllAsync();
            var mappedAnswers = answers == null ? null : mapper.Map<List<AnswerDTO>>(answers);
            if (mappedAnswers is not null)
            {
                await _cacheService.CacheDataAsync(cacheKey, mappedAnswers);
            }

            return mappedAnswers;
        }

        public async Task<AnswerDTO>? FindAnswerByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<AnswerDTO>(cacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }
            
            var answer = await repoManager.Answer.GetAsync(id);
            var mappedAnswer = answer == null ? null : mapper.Map<AnswerDTO>(answer);
            if (mappedAnswer is not null)
            {
                await _cacheService.CacheDataAsync(cacheKey, mappedAnswer);
            }

            return mappedAnswer;
        }

        public async Task<AnswerDTO> AddAnswerAsync(AnswerCreationDTO answerDto)
        {
            var answerToCreate = mapper.Map<Answer>(answerDto);
            var createdAnswer = await repoManager.Answer.AddAsync(answerToCreate);
            return mapper.Map<AnswerDTO>(createdAnswer);
        }

        public async Task<AnswerDTO> UpdateAnswerAsync(int id, AnswerUpdateDTO answerUpdateDto)
        {
            var answerToUpdate = mapper.Map<Answer>(answerUpdateDto);
            var updated = await repoManager.Answer.UpdateAsync(id, answerToUpdate);
            var updatedMappedAnswer = updated == null ? null : mapper.Map<AnswerDTO>(updated);

            if (updatedMappedAnswer is not null)
            {
                var cacheKey = GetCacheKey(id.ToString());
                await _cacheService.CacheDataAsync(cacheKey, updatedMappedAnswer);
            }

            return updatedMappedAnswer;
        }

        public async Task<Answer> DeleteAnswerAsync(int id)
        {
            var deletedAnswer = await repoManager.Answer.DeleteAsync(id);
            if (deletedAnswer is not null)
            {
                var cacheKey = GetCacheKey(id.ToString());
                await _cacheService.DeleteCachedData(cacheKey);
            }

            return deletedAnswer;
        }
    }
}
