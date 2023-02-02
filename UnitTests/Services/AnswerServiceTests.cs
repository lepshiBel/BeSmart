using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class AnswerServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceAnswer service;
        private readonly IRepositoryManager repoManager;

        public AnswerServiceTests()
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

            service = new AnswerService(repoManager, mapper);

            if (!context.Answers.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Answer>()
                {
                    new() {Id = 1, Text = "test", QuestionId = 1, Fidelity = false},
                    new() {Id = 2, Text = "test2", QuestionId = 1, Fidelity = true},
                    new() {Id = 3, Text = "test3", QuestionId = 1, Fidelity = false},
                    new() {Id = 4, Text = "test4", QuestionId = 2, Fidelity = false},
                    new() {Id = 5, Text = "test5", QuestionId = 2, Fidelity = false},
                    new() {Id = 6, Text = "test6", QuestionId = 2, Fidelity = true}
                };

            context.Answers.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddAnswer()
        {
            // Arrange
            var data = new AnswerCreationDTO() { Fidelity = true, QuestionId = 3, Text = "newAnswer" };

            // Act
            await service.AddAnswerAsync(data);

            // Assert
            var actual = context.Answers.FirstOrDefault(e => e.Id == 7);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteAnswer()
        {
            // Act
            await service.DeleteAnswerAsync(1);

            // Assert
            var actual = context.Answers.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetAnswer()
        {
            // Act
            var data = await service.FindAnswerByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetAnswer()
        {
            // Act
            var data = await service.FindAnswerByIdAsync(10);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldUpdateAnswer()
        {
            // Arrange
            var newValue = "updatedText";
            var data = new AnswerUpdateDTO() { Fidelity = true, Text = newValue };
            var old = context.Answers.FirstOrDefault(e => e.Id == 1);

            // Act
            await service.UpdateAnswerAsync(old.Id, data);

            // Assert
            var actual = context.Answers.First(e => e.Id == 1);
            Assert.Equal(actual.Text, newValue);
        }

        [Fact]
        public async Task ShouldReturnAllAnswers()
        {
            // Act
            var data = await service.GetAllAnswersAsync();

            // Assert
            Assert.Equal(6, data.Count);
        }
    }
}

