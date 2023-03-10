using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceLesson
    {
        Task<List<LessonDTO>> GetAllLessonsAsync();
        Task<LessonDTO> FindLessonByIdAsync(int id);
        Task<LessonWithCardsDTO> GetLessonWithCardsAsync(int id);
        Task<LessonDTO> AddLessonAsync(LessonCreationDTO lessonCreationDto);
        Task<LessonDTO> UpdateLessonAsync(int id, LessonDTO lessonDto);
        Task<Lesson> DeleteLessonAsync(int id);
    }
}
