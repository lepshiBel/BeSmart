using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class TestService : IServiceTest
    {
        private readonly IRepositoryManager repoManager;

        public TestService(IRepositoryManager repoManager)
        {
            this.repoManager = repoManager;
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await repoManager.Test.GetAllAsync();
        }

        public async Task<Test> FindTestByIdAsync(int id)
        {
            return await repoManager.Test.GetAsync(id);
        }
        public async Task<Test> GetTestWithQuestionsAsync(int id)
        {
            return await repoManager.Test.GetTestWithQuestionsAsync(id);
        }

        public async Task<Test> AddTestAsync(Test test)
        {
            return await repoManager.Test.AddAsync(test);
        }

        public async Task<Test> UpdateTestAsync(Test test)
        {

            return await repoManager.Test.UpdateAsync(test);
        }

        public async Task<Test> DeleteTestAsync(int id)
        {
            return await repoManager.Test.DeleteAsync(id);
        }
    }
}
