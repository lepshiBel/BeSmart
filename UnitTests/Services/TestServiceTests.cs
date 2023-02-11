using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Test;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class TestServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceTest service;
        private readonly IRepositoryManager repoManager;

        public TestServiceTests()
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

            service = new TestService(repoManager, mapper);

            if (!context.Tests.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Test>()
            {
                new() { Id = 1, Name = "test", QuestionsCount = 3, ThemeId = 1 },
                new() { Id = 2, Name = "test2", QuestionsCount = 5, ThemeId = 1 },
                new() { Id = 3, Name = "test3", QuestionsCount = 2, ThemeId = 1 },
                new() { Id = 4, Name = "test4", QuestionsCount = 6, ThemeId = 2 },
                new() { Id = 5, Name = "test5", QuestionsCount = 1, ThemeId = 2 },
                new() { Id = 6, Name = "test6", QuestionsCount = 10, ThemeId = 2 }
            };

            context.Tests.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddTest()
        {
            // Arrange
            var data = new TestCreationDTO() { Name = "newTest", QuestionsCount = 10, ThemeId = 3 };

            // Act
            await service.AddTestAsync(data);

            // Assert
            var actual = context.Tests.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteTest()
        {
            // Act
            await service.DeleteTestAsync(1);

            // Assert
            var actual = context.Tests.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetTest()
        {
            // Act
            var data = await service.FindTestByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetTest()
        {
            // Act
            var data = await service.FindTestByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldGetTestWithQuestions()
        {
            // Act
            var data = await service.GetTestWithQuestionsAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldUpdateTest()
        {
            // Arrange
            var newName = "updatedName";
            var newCountquestion = 9;
            var data = new TestUpdateDTO() { Name = newName, Countquestion = newCountquestion };
            var old = context.Tests.FirstOrDefault(e => e.Id == 1);

            // Act
            await service.UpdateTestAsync(old.Id, data);

            // Assert
            var actual = context.Tests.First(e => e.Id == 1);
            Assert.Equal(actual.Name, newName);
            Assert.Equal(actual.QuestionsCount, newCountquestion);
        }

        [Fact]
        public async Task ShouldReturnAllTests()
        {
            // Act
            var data = await service.GetAllTestsAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }
    }
}
