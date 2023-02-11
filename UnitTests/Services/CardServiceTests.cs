using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Xunit;

namespace UnitTests.Services
{
    public class CardServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceCard service;
        private readonly IRepositoryManager repoManager;

        public CardServiceTests()
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

            service = new CardService(repoManager, mapper);

            if (!context.Cards.Any())
            {
                SeedData(context);
            }
        }

        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Card>()
                {
                    new() {Id = 1, Word = "test", Text = "test", ImageUrl = "", Transctipt = "[test]", LessonId = 1},
                    new() {Id = 2, Word = "test2", Text = "test2", ImageUrl = "", Transctipt = "[test2]", LessonId = 1},
                    new() {Id = 3, Word = "test3", Text = "test3", ImageUrl = "", Transctipt = "[test3]", LessonId = 1},
                    new() {Id = 4, Word = "test4", Text = "test4", ImageUrl = "", Transctipt = "[test4]", LessonId = 2},
                    new() {Id = 5, Word = "test5", Text = "test5", ImageUrl = "", Transctipt = "[test5]", LessonId = 2},
                    new() {Id = 6, Word = "test6", Text = "test6", ImageUrl = "", Transctipt = "[test6]", LessonId = 2}
                };

            context.Cards.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddCard()
        {
            // Arrange
            var data = new CardCreationDTO() { Word = "test7", Text = "test7", ImageUrl = "", Transctipt = "[test7]", LessonId = 2};

            // Act
            await service.AddCardAsync(data);

            // Assert
            var actual = context.Cards.FirstOrDefault(e => e.Id == 7);

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteCard()
        {
            // Act
            await service.DeleteCardAsync(1);

            // Assert
            var actual = context.Cards.FirstOrDefault(e => e.Id == 1);

            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldGetCard()
        {
            // Act
            var data = await service.FindCardByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetCard()
        {
            //Act
            var data = await service.FindCardByIdAsync(10);

            //Assert
            Assert.Null(data);
        }

        [Fact]
        public async Task ShouldGetAllCardsAsync()
        {
            //Act
            var data = await service.GetAllCardsAsync();

            //Assert
            Assert.Equal(6, data.Count);
        }

        [Fact]
        public async Task ShouldUpdateCardAsynce()
        {
            //Arange 
            var newValueForWord = "updatedWord";
            var newValueForText = "updatedText";
            var newValueForImageUrl = "";
            var newValueForTranscript = "updatedTranscript";

            var data = new CardUpdateDTO() { 
                Word = newValueForWord, 
                Text = newValueForText, 
                ImageUrl = newValueForImageUrl, 
                Transctipt = newValueForTranscript};

            var old = context.Cards.FirstOrDefault(e => e.Id == 1);

            //Act
            await service.UpdateCardAsync(old.Id, data);

            //Asert
            var actual = context.Cards.First(e => e.Id == 1);

            Assert.Equal(newValueForWord, actual.Word);
            Assert.Equal(newValueForText, actual.Text);
            Assert.Equal(newValueForImageUrl, actual.ImageUrl);
            Assert.Equal(newValueForTranscript, actual.Transctipt);           
        }
    }
}
