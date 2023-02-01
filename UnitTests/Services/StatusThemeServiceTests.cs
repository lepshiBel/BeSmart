using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class StatusThemeServiceTests : IDisposable
    {
        private readonly BeSmartDbContext context;
        private readonly IRepositoryManager manager;
        private readonly IServiceStatusTheme service;

        public StatusThemeServiceTests()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("test")
               .Options;
            context = new BeSmartDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            manager = new RepositoryManager(context);

            manager = new RepositoryManager(context);
            var configuration = new MapperConfiguration(conf =>
                     conf.AddMaps(typeof(AutoMapperProfile).Assembly));
            var mapper = new Mapper(configuration);


            service = new StatusThemeService(manager, mapper);

            if (!context.StatusThemes.Any())
            {
                SeedData(context);
            }
        }

        private void SeedData(BeSmartDbContext context)
        {
            var statusThemes = new List<StatusTheme>()
            {
                new() { Id = 1, Status = "Не пройдено", AmountOfCompletedLessons = 1, MembershipId = 1, ThemeId = 1 },
                new() { Id = 2, Status = "Не пройдено", AmountOfCompletedLessons = 2, MembershipId = 1, ThemeId = 1 },
                new() { Id = 3, Status = "Не пройдено", AmountOfCompletedLessons = 3, MembershipId = 1, ThemeId = 1 },
                new() { Id = 4, Status = "Не пройдено", AmountOfCompletedLessons = 4, MembershipId = 2, ThemeId = 2 },
                new() { Id = 5, Status = "Не пройдено", AmountOfCompletedLessons = 5, MembershipId = 2, ThemeId = 2 },
                new() { Id = 6, Status = "Не пройдено", AmountOfCompletedLessons = 6, MembershipId = 2, ThemeId = 2 }
            };

            var themes = new List<Theme>()
            {
                new() { Id = 1, Name = "theme1", CountLesson = 5, CountTest = 5, CourseId = 1 },
                new() { Id = 2, Name = "theme2", CountLesson = 3, CountTest = 3, CourseId = 1 },
            };

            var statusLessons = new List<StatusLesson>()
            {
                new() { Id = 1, Status = "Не пройдено", StatusThemeId = 1, LessonId = 1 },
                new() { Id = 2, Status = "Не пройдено", StatusThemeId = 2, LessonId = 1 },
                new() { Id = 3, Status = "Не пройдено", StatusThemeId = 3, LessonId = 1 },
                new() { Id = 4, Status = "Не пройдено", StatusThemeId = 4, LessonId = 2 },
                new() { Id = 5, Status = "Не пройдено", StatusThemeId = 5, LessonId = 2 },
                new() { Id = 6, Status = "Не пройдено", StatusThemeId = 6, LessonId = 2 }
            };

            var lessons = new List<Lesson>()
            {
                new() { Id = 1, Name = "lesson1", Text = "someText1", ThemeId = 1 },
                new() { Id = 2, Name = "lesson2", Text = "someText2", ThemeId = 2 },
            };

            context.StatusThemes.AddRange(statusThemes);
            context.Themes.AddRange(themes);
            context.StatusLessons.AddRange(statusLessons);
            context.Lessons.AddRange(lessons);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task ShouldCheckIfThemeStarted()
        {
            // Act
            var data = await service.CheckIfThemeStarted(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldGetStatusThemeWithStatusLessons()
        {
            // Act
            var data = await service.GetStatusThemeWithStatusLessons(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldStartNewThemeAsync()
        {
            // Arrange
            var data = new StatusTheme() { Id = 1, Status = "Не пройдено", AmountOfCompletedLessons = 5, MembershipId = 1, ThemeId = 1 };
           
            // Act
            var result = await service.StartNewThemeAsync(data);

            // Assert
            Assert.NotNull(result);
        }
    }
}