using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IThemeRepository : IRepositoryBase<Theme>
    {
        Task<Theme> GetThemeWithLessonsAsync(int id);
        Task<Theme> GetThemeWithTestsAsync(int id);
    }
}
