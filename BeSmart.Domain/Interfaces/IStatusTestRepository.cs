using BeSmart.Domain.Models;
namespace BeSmart.Domain.Interfaces
{
    public interface IStatusTestRepository : IRepositoryBase<StatusTest>
    {
        Task<StatusTest> AddStatusTestAsync(int testId, int statusThemeId);
    }
}
