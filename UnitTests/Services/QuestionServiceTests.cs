using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Answer;
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
    public class QuestionServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceQuestion service;
        private readonly IRepositoryManager repoManager;

        public QuestionServiceTests()
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

            service = new QuestionService(repoManager, mapper);

            if (!context.Questions.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Question>()
                {
                    new() { Id = 1, Text = "test", TestId = 1 },
                    new() { Id = 2, Text = "test2", TestId = 1 },
                    new() { Id = 3, Text = "test3", TestId = 1 },
                    new() { Id = 4, Text = "test4", TestId = 2 },
                    new() { Id = 5, Text = "test5", TestId = 2 },
                    new() { Id = 6, Text = "test6", TestId = 2 }
                };

            context.Questions.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddQuestion()
        {
            // Arrange
            var data = new QuestionCreationDTO() { TestId = 3, Text = "newQuestion" };

            // Act
            await service.AddQuestionAsync(data);

            // Assert
            var actual = context.Questions.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteQuestion()
        {
            // Act
            await service.DeleteQuestionAsync(1);

            // Assert
            var actual = context.Questions.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetQuestion()
        {
            // Act
            var data = await service.FindQuestionByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetQuestion()
        {
            // Act
            var data = await service.FindQuestionByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldGetQuestionWithAnswers()
        {
            // Act
            var data = await service.GetQuestionWithAnswersAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldUpdateQuestion()
        {
            // Arrange
            var newValue = "updatedText";
            var data = new QuestionCreationDTO() { Text = newValue };
            var old = context.Questions.FirstOrDefault(e => e.Id == 1);

            // Act
            await service.UpdateQuestionAsync(old.Id, data);

            // Assert
            var actual = context.Questions.First(e => e.Id == 1);
            Assert.Equal(actual.Text, newValue);
        }

        [Fact]
        public async Task ShouldReturnAllQuestions()
        {
            // Act
            var data = await service.GetAllQuestionsAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }
    }
}
