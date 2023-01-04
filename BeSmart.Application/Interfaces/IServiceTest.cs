using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceTest
    {
        Task<List<Test>> GetAllTestsAsync();
        Task<Test> FindTestByIdAsync(int id);
        Task<Test> GetTestWithQuestionsAsync(int id);
        Task<Test> AddTestAsync(Test test);
        Task<Test> UpdateTestAsync(Test test);
        Task<Test> DeleteTestAsync(int id);
    }
}
