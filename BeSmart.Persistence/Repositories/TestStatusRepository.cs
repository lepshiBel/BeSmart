using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BeSmart.Persistence.Repositories
{
    public class TestStatusRepository : RepositoryBase<StatusTest, BeSmartDbContext>, IStatusTestRepository
    {
        public TestStatusRepository(BeSmartDbContext context) : base(context) { }

        public async Task<StatusTest> AddStatusTestAsync(int testId, int statusThemeId)
        {
            var statusTest = new StatusTest() { TestId = testId, StatusThemeId = statusThemeId };
            await context.StatusTests.AddAsync(statusTest);
            await context.SaveChangesAsync();
            return statusTest;
        }

        public async Task<StatusTest> GetStatusTestWithTest(int statusTestId)
        {
            return await context.StatusTests.Include(x => x.Test).FirstOrDefaultAsync(x=>x.Id == statusTestId);
        }

        public async Task<StatusTest> UpdateAnswerFieldInStatusTestAsync(bool fidelity, int statusTestId)
        {
            var statusTest = await context.StatusTests.FindAsync(statusTestId);
            if (statusTest == null) return null;

            if (fidelity) statusTest.AmountOfFaithfullAnswers += 1;
                else statusTest.AmountOfIncorrectAnswers += 1;

            var updated = await base.UpdateAsync(statusTest.Id, statusTest);
            return updated;
        }
        public async Task<StatusTest> UpdateStatusAndMarkInStatusTestAsync(StatusTest statusTest)
        {
            statusTest.Status = "Пройден";
            var updated = await base.UpdateAsync(statusTest.Id, statusTest);
            return updated;
        }
    }
}
