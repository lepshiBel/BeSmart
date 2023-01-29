using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class StatusLessonServiceTest : IDisposable
    {
        private readonly BeSmartDbContext context;
        private readonly IRepositoryManager manager;
        private readonly IServiceStatusLesson service;

        public StatusLessonServiceTest()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("test")
               .Options;
            context = new BeSmartDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            manager = new RepositoryManager(context);
            service = new StatusLessonService(manager);

            if (!context.StatusLessons.Any())
            {
                SeedData(context);
            }

        }

        private void SeedData(BeSmartDbContext context)
        {
            var statusLessons = new List<StatusLesson>()
            {
                new() { Id = 1, Status="Пройден", StatusThemeId=1, LessonId = 5},
                new() { Id = 2, Status="Не пройдено", StatusThemeId=1, LessonId = 6},
                new() { Id = 3, Status="Не пройдено", StatusThemeId=1, LessonId = 7},
                new() { Id = 4, Status="Не пройдено", StatusThemeId=1, LessonId = 8}
            };

            var statusThemes = new List<StatusTheme>()
            {
                new() { Id = 1, Status="В процессе", MembershipId=1, ThemeId = 1, AmountOfCompletedLessons=1},
                new() { Id = 2, Status="Не пройдено",  MembershipId=2, ThemeId = 1, AmountOfCompletedLessons=0},
            };

            var themes = new List<Theme>()
            {
                new() { Id = 1, Name="theme1", CountLesson=10, CountTest = 3, CourseId=1},
                new() { Id = 2, Name="theme1", CountLesson=5, CountTest = 1, CourseId=1},
            };

            context.StatusLessons.AddRange(statusLessons);
            context.StatusThemes.AddRange(statusThemes);
            context.Themes.AddRange(themes);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldPassTheLesson()
        {
            // Act
            var updated = await service.PassTheLesson(2);

            //Assert
            Assert.Equal(updated.Status, "Пройден");
        }

        [Fact]
        public async Task ShouldNotPassTheLesson()
        {
            // Act
            var updated = await service.PassTheLesson(1);

            //Assert
            Assert.Null(updated);
        }

        // todo CheckIfThemeIsCompletedAsync

        //[Fact]
        //public async Task ShouldCheckIfThemeIsCompleted()
        //{
        //    // Arrange
        //    var updated = await service.PassTheLesson(2);

        //    // Act
        //    var checkedTheme = await service.CheckIfThemeIsCompletedAsync(updated);

        //    //Assert
        //    Assert.NotNull(checkedTheme);
        //}

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
