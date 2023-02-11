using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Test;
using BeSmart.Domain.DTOs.Theme;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class ThemeServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceTheme service;
        private readonly IRepositoryManager repoManager;

        public ThemeServiceTests()
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

            service = new ThemeService(repoManager, mapper);

            if (!context.Themes.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Theme>()
            {
                new() { Id = 1, Name = "test", CountLesson = 5, CountTest = 5, CourseId = 1 },
                new() { Id = 2, Name = "test2", CountLesson = 4, CountTest = 3, CourseId = 1 },
                new() { Id = 3, Name = "test3", CountLesson = 5, CountTest = 5, CourseId = 1 },
                new() { Id = 4, Name = "test4", CountLesson = 3, CountTest = 2, CourseId = 2 },
                new() { Id = 5, Name = "test5", CountLesson = 5, CountTest = 2, CourseId = 2 },
                new() { Id = 6, Name = "test6", CountLesson = 3, CountTest = 3, CourseId = 2 }
            };

            context.Themes.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddTheme()
        {
            // Arrange
            var data = new ThemeCreationDTO() { Name = "newTest", CountLesson = 4, CountTest = 3, CourseId = 3 };

            // Act
            await service.AddThemeAsync(data);

            // Assert
            var actual = context.Themes.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteTheme()
        {
            // Act
            await service.DeleteThemeAsync(1);

            // Assert
            var actual = context.Themes.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetTheme()
        {
            // Act
            var data = await service.FindThemeByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetTheme()
        {
            // Act
            var data = await service.FindThemeByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldGetThemeWithLessons()
        {
            // Act
            var data = await service.GetThemeWithLessonsAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldGetThemeWithTests()
        {
            // Act
            var data = await service.GetThemeWithTestsAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldUpdateTheme()
        {
            // Arrange
            var newName = "updatedName";
            var newCountLesson = 4;
            var newCountTest = 3;
            var newCourseId = 1;
            var data = new ThemeCreationDTO() { Name = newName, CountLesson = newCountLesson, CountTest = newCountTest, CourseId = newCourseId };
            var old = context.Themes.FirstOrDefault(e => e.Id == 1);

            // Act
            await service.UpdateThemeAsync(old.Id, data);

            // Assert
            var actual = context.Themes.First(e => e.Id == 1);
            Assert.Equal(actual.Name, newName);
            Assert.Equal(actual.CountLesson, newCountLesson);
            Assert.Equal(actual.CountTest, newCountTest);
            Assert.Equal(actual.CourseId, newCourseId);
        }

        [Fact]
        public async Task ShouldReturnAllThemes()
        {
            // Act
            var data = await service.GetAllThemesAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }
    }
}
