using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class StatusThemeService : IServiceStatusTheme
    {
        private readonly IRepositoryManager repositoryManger;

        public StatusThemeService(IRepositoryManager repositoryManger)
        {
            this.repositoryManger = repositoryManger;
        }

        public async Task<StatusTheme> AddStatusThemeAsync(int themeId, int membershipId)
        {
            var createdStatus = await repositoryManger.StatusTheme.AddStatusTheme(themeId, membershipId);
            return createdStatus;
        }
    }
}
