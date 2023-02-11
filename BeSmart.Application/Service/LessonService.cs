using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class LessonService : IServiceLesson
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public LessonService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<LessonDTO>> GetAllLessonsAsync()
        {
            var lessons = await repoManager.Lesson.GetAllAsync();
            return lessons == null ? null : mapper.Map<List<LessonDTO>>(lessons);
        }

        public async Task<LessonDTO> FindLessonByIdAsync(int id)
        {
            var lesson = await repoManager.Lesson.GetAsync(id);
            return lesson == null ? null : mapper.Map<LessonDTO>(lesson);
        }

        public async Task<LessonWithCardsDTO> GetLessonWithCardsAsync(int id)
        {
            var lessonWithCards = await repoManager.Lesson.GetLessonWithCardsAsync(id);
            return lessonWithCards == null ? null : mapper.Map<LessonWithCardsDTO>(lessonWithCards);
        }

        public async Task<LessonDTO> AddLessonAsync(LessonCreationDTO lessonCreationDto)
        {
            var lessonToCreate = mapper.Map<Lesson>(lessonCreationDto);
            var createdLesson = await repoManager.Lesson.AddAsync(lessonToCreate);
            return createdLesson == null ? null : mapper.Map<LessonDTO>(createdLesson);
        }

        public async Task<LessonDTO> UpdateLessonAsync(int id, LessonCreationDTO lessonDto)
        {
            var lesson = mapper.Map<Lesson>(lessonDto);
            var updated = await repoManager.Lesson.UpdateAsync(id, lesson);
            return updated == null ? null : mapper.Map<LessonDTO>(updated);
        }

        public async Task<Lesson> DeleteLessonAsync(int id)
        {
            return await repoManager.Lesson.DeleteAsync(id);
        }
    }
}
