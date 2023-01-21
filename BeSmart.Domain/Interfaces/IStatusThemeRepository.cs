using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusThemeRepository : IRepositoryBase<StatusTheme>
    {
        public Task<StatusTheme> AddStatusTheme(int themeId, int membershipId);
    }
}
