using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface ITestRepository : IRepositoryBase<Test>
    {
        Task<Test> GetTestWithQuestionsAsync(int id);
    }
}
