using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.StatusTest;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusTest
    {
        public Task<StatusTest> StartTestAsync(int testId, int statusThemeId);
        public Task<StatusTest> FihishTheAttemptAsync(int statusTestId);
        public Task<StatusTestDTO> FihishTheTestAsync(int statusTestId);
        public int CalculateTheMark(int total, int? faithfull, int? incorrect);
    }
}
