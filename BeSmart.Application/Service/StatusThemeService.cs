using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.StatusTheme;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class StatusThemeService : IServiceStatusTheme
    {
        private readonly IRepositoryManager repositoryManger;
        private readonly IMapper mapper;

        public StatusThemeService(IRepositoryManager repositoryManger, IMapper mapper)
        {
            this.repositoryManger = repositoryManger;
            this.mapper = mapper;
        }

        public async Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId)
        {
            var statusTheme = await repositoryManger.StatusTheme.GetAsync(statusThemeId);

            if (statusTheme == null) return null;

            return statusTheme.Status == "В процессе" ? null : statusTheme;
        }

        public async Task<StatusThemeWithLessonsDTO> GetStatusThemeWithStatusLessons(int statusThemeId)
        {
            var result = await repositoryManger.StatusTheme.GetStatusThemeWithStatusLessonsWithLessonsAsync(statusThemeId);

            if(result == null) return null;

            var mapped = mapper.Map<StatusThemeWithLessonsDTO>(result);

            return mapped;
        }

        // обновляет статус темы на в процессе и добавляет записи со статусом не пройдено в таблицу статус_уроков
        public async Task<StatusTheme> StartNewThemeAsync(StatusTheme existed)
        {
            var updated = await repositoryManger.StatusTheme.UpdateStatusTheme(existed.Id, "В процессе");

            var themeWithLessons = await repositoryManger.Theme.GetThemeWithLessonsAsync(updated.ThemeId);

            foreach (var lesson in themeWithLessons.Lessons)
            {
                await repositoryManger.StatusLesson.AddStatusLessonAsync(lesson.Id, updated.Id);
            }

            return updated;
        }
    }
}
