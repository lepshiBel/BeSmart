using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Test;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class TestService : IServiceTest
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        private readonly ICacheService _cacheService;

        public TestService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }
        
        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(TestService), dataId);
        }

        public async Task<List<TestDTO>> GetAllTestsAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<TestDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var tests = await repoManager.Test.GetAllAsync();
            var mappedResult = tests == null ? null : mapper.Map<List<TestDTO>>(tests);   
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<TestDTO> FindTestByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<TestDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var test = await repoManager.Test.GetAsync(id);
            var mappedResult = test == null ? null : mapper.Map<TestDTO>(test);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<TestWithQuestionsDTO> GetTestWithQuestionsAsync(int id)
        {
            var cacheKey = GetCacheKey($"-with-questions-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<TestWithQuestionsDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var testWithQuestions = await repoManager.Test.GetTestWithQuestionsAsync(id);
            var mappedResult = testWithQuestions == null ? null :mapper.Map<TestWithQuestionsDTO>(testWithQuestions);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<TestDTO> AddTestAsync(TestCreationDTO testCreationDto)
        {
            var testToCreate = mapper.Map<Test>(testCreationDto);
            var createdTest = await repoManager.Test.AddAsync(testToCreate);
            return createdTest == null ? null : mapper.Map<TestDTO>(createdTest);
        }

        public async Task<TestDTO> UpdateTestAsync(int id, TestUpdateDTO testDto)
        {
            var test = mapper.Map<Test>(testDto);
            var updated = await repoManager.Test.UpdateAsync(id, test);
            var mappedResult = updated == null ? null : mapper.Map<TestDTO>(updated);
            
            var cacheKey = GetCacheKey(id.ToString());
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }


        public async Task<Test> DeleteTestAsync(int id)
        {
            var result = await repoManager.Test.DeleteAsync(id);
            
            var cacheKey = GetCacheKey(id.ToString());
            await _cacheService.DeleteCachedData(cacheKey);

            return result;
        }
    }
}
