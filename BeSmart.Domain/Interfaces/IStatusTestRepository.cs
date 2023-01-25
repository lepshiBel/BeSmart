using BeSmart.Domain.Models;
namespace BeSmart.Domain.Interfaces
{
    public interface IStatusTestRepository : IRepositoryBase<StatusTest>
    {
        Task<StatusTest> AddStatusTestAsync(int testId, int statusThemeId);
        Task<StatusTest> UpdateAnswerFieldInStatusTestAsync(bool fidelity, int statusTestId);
        Task<StatusTest> UpdateStatusAndMarkInStatusTestAsync(StatusTest statusTest);
        Task<StatusTest> GetStatusTestWithTest(int statusTestId);
    }
}
