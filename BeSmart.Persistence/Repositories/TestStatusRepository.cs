using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

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
    }
}
