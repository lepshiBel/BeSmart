using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class CourseServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceCourse service;
        private readonly IRepositoryManager repoManager;

        public CourseServiceTests()
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

            service = new CourseService(repoManager, mapper);

            if (!context.Courses.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Course>()
            {
                new() { Id = 1, Name = "test", CountOfThemes = 5, CreatedById = 1, CategoryId = 1 },
                new() { Id = 2, Name = "test2", CountOfThemes = 4, CreatedById = 1, CategoryId = 1 },
                new() { Id = 3, Name = "test3", CountOfThemes = 5, CreatedById = 1, CategoryId = 1 },
                new() { Id = 4, Name = "test4", CountOfThemes = 3, CreatedById = 2, CategoryId = 2 },
                new() { Id = 5, Name = "test5", CountOfThemes = 5, CreatedById = 2, CategoryId = 2 },
                new() { Id = 6, Name = "test6", CountOfThemes = 5, CreatedById = 2, CategoryId = 2 }
            };

            context.Courses.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddCourse()
        {
            // Arrange
            var data = new CourseCreationDTO() { Name = "newName", CountOfThemes = 3, CreatedById = 1, CategoryId = 2 };

            // Act
            await service.AddCourseAsync(data);

            // Assert
            var actual = context.Courses.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteCourse()
        {
            // Act
            await service.DeleteCourseAsync(1);

            // Assert
            var actual = context.Courses.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetCourse()
        {
            // Act
            var data = await service.FindCourseByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetCourse()
        {
            // Act
            var data = await service.FindCourseByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldGetCourseWithThemes()
        {
            // Act
            var data = await service.GetCourseWithThemesAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldUpdateCourse()
        {
            // Arrange
            var newName = "updatedName";
            var newCountOfThemes = 4;
            var newCategoryId = 1;
            var data = new CourseUpdateDTO() { Name = newName, CountOfThemes = newCountOfThemes, CategoryId = newCategoryId };
            var old = context.Courses.FirstOrDefault(e => e.Id == 2);

            // Act
            await service.UpdateCourseAsync(old.Id, data);

            // Assert
            var actual = context.Courses.First(e => e.Id == 2);
            Assert.Equal(actual.Name, newName);
            Assert.Equal(actual.CountOfThemes, newCountOfThemes);
            Assert.Equal(actual.CategoryId, newCategoryId);
        }

        [Fact]
        public async Task ShouldReturnAllCourses()
        {
            // Act
            var data = await service.GetAllCoursesAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }
    }
}
