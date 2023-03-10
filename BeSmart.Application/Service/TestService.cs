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

        public TestService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<TestDTO>> GetAllTestsAsync()
        {
            var tests = await repoManager.Test.GetAllAsync();
            return tests == null ? null : mapper.Map<List<TestDTO>>(tests);   
        }

        public async Task<TestDTO> FindTestByIdAsync(int id)
        {
            var test = await repoManager.Test.GetAsync(id);
            return test == null ? null : mapper.Map<TestDTO>(test);
        }

        public async Task<TestWithQuestionsDTO> GetTestWithQuestionsAsync(int id)
        {
            var testWithQuestions = await repoManager.Test.GetTestWithQuestionsAsync(id);
            return testWithQuestions == null ? null :mapper.Map<TestWithQuestionsDTO>(testWithQuestions);
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
            return updated == null ? null : mapper.Map<TestDTO>(updated);
        }


        public async Task<Test> DeleteTestAsync(int id)
        {
            return await repoManager.Test.DeleteAsync(id);
        }
    }
}
