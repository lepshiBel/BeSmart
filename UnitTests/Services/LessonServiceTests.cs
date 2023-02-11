using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class LessonServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceLesson service;
        private readonly IRepositoryManager repoManager;

        public LessonServiceTests()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("test")
               .Options;

            var dbContext = new BeSmartDbContext(options);
            this.context = dbContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repoManager = new RepositoryManager(context);
            var configuration = new MapperConfiguration(conf =>
                     conf.AddMaps(typeof(AutoMapperProfile).Assembly));
            var mapper = new Mapper(configuration);

            service = new LessonService(repoManager, mapper);

            if (!context.Lessons.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Lesson>()
                {
                    new() {Id = 1, Name = "test", Text = "test", ThemeId = 1},
                    new() {Id = 2, Name = "test2", Text = "test2", ThemeId = 1},
                    new() {Id = 3, Name = "test3", Text = "test3", ThemeId = 1},
                    new() {Id = 4, Name = "test4", Text = "test4", ThemeId = 2},
                    new() {Id = 5, Name = "test5", Text = "test5", ThemeId = 2},
                    new() {Id = 6, Name = "test6", Text = "test6", ThemeId = 2}
                };

            context.Lessons.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddLesson()
        {
            // Arrange
            var data = new LessonCreationDTO() { Text = "test7", Name = "test7", ThemeId = 1 };

            // Act
            await service.AddLessonAsync(data);

            // Assert
            var actual = context.Lessons.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteLesson()
        {
            // Act
            await service.DeleteLessonAsync(1);

            // Assert
            var actual = context.Lessons.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetLesson()
        {
            // Act
            var data = await service.FindLessonByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetLesson()
        {
            // Act
            var data = await service.FindLessonByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldUpdateLessonAsync()
        {
            // Arrange
            var newValueForName = "updatedName";
            var newValueForText = "updatedText";
            var newValueForThemeId = 2;
            var data = new LessonCreationDTO() { Name = newValueForName, Text = newValueForText, ThemeId = newValueForThemeId };
            var old = context.Lessons.FirstOrDefault(e => e.Id == 1);

            // Act
            await service.UpdateLessonAsync(old.Id, data);

            // Assert
            var actual = context.Lessons.First(e => e.Id == 1);
            Assert.Equal(actual.Name, newValueForName);
            Assert.Equal(actual.Text, newValueForText);
            Assert.Equal(actual.ThemeId, newValueForThemeId);
        }

        [Fact]
        public async Task ShouldGetAllLessonsAsync()
        {
            // Act
            var data = await service.GetAllLessonsAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }

        [Fact]
        public async Task ShouldGetLessonWithCardsAsync()
        {
            // Act
            var data = await service.GetLessonWithCardsAsync(1);

            // Assert
            Assert.NotNull(data);
        }
    }
}
