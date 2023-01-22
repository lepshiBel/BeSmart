using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusLesson
    {
        Task<StatusLesson> AddStatusLessonAsync(int lessonId, int statusThemeId);
    }
}
