using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Theme;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class ThemeService : IServiceTheme
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        public ThemeService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }
        public async Task<List<ThemeDTO>> GetAllThemesAsync()
        {
            var themes = await repoManager.Theme.GetAllAsync();
            return themes == null ? null : mapper.Map<List<ThemeDTO>>(themes);
        }
        public async Task<ThemeDTO> FindThemeByIdAsync(int id)
        {
            var theme = await repoManager.Theme.GetAsync(id);
            return theme == null ? null : mapper.Map<ThemeDTO>(theme);
        }
        public async Task<ThemeWithLessonsDTO> GetThemeWithLessonsAsync(int id)
        {
            var themesWithLessons = await repoManager.Theme.GetThemeWithLessonsAsync(id);
            return themesWithLessons == null ? null : mapper.Map<ThemeWithLessonsDTO>(themesWithLessons);
        }
        public async Task<ThemeWithTestsDTO> GetThemeWithTestsAsync(int id)
        {
            var themesWithTests = await repoManager.Theme.GetThemeWithTestsAsync(id);
            return themesWithTests == null ? null : mapper.Map<ThemeWithTestsDTO>(themesWithTests);
        }

        public async Task<ThemeDTO> AddThemeAsync(ThemeCreationDTO themeCreationDto)
        {
            var theme = mapper.Map<Theme>(themeCreationDto);
            var created = await repoManager.Theme.AddAsync(theme);
            return mapper.Map<ThemeDTO>(created);
        }

        public async Task<ThemeDTO> UpdateThemeAsync(int id, ThemeCreationDTO themeUpdateDto)
        {
            var theme = mapper.Map<Theme>(themeUpdateDto);
            var updated = await repoManager.Theme.UpdateAsync(id, theme);
            return updated == null ? null : mapper.Map<ThemeDTO>(updated);
        }
        public async Task<Theme> DeleteThemeAsync(int id)
        {
            return await repoManager.Theme.DeleteAsync(id);
        }
    }
}
