using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusTest
    {
        Task<StatusTest> AddStatusTestAsync(int testId, int statusThemeId);
    }
}
