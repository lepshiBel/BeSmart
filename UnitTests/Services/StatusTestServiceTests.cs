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
    public class StatusTestServiceTests : IDisposable
    {

        private readonly IServiceStatusTest service;
        private readonly BeSmartDbContext context;
        private readonly IRepositoryManager manager;

        public StatusTestServiceTests()
        {
            var options = new DbContextOptionsBuilder()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;

            var dbContext = new BeSmartDbContext(options);
            context = dbContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            manager = new RepositoryManager(context);

            var configuration = new MapperConfiguration(conf =>
                     conf.AddMaps(typeof(AutoMapperProfile).Assembly));
            var mapper = new Mapper(configuration);

            service = new StatusTestService(manager, mapper);

            if (!context.StatusTests.Any())
            {
                SeedData(context);
            }
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private void SeedData(BeSmartDbContext context)
        {
            var statusTests = new List<StatusTest>() {
                new StatusTest() { Id = 1, Status="Не пройден", AmountOfFaithfullAnswers=0, AmountOfIncorrectAnswers=0, Mark=null, StatusThemeId=1, TestId=1 },
                new StatusTest() { Id = 2, Status="Не пройден", AmountOfFaithfullAnswers=0, AmountOfIncorrectAnswers=0, Mark=null, StatusThemeId=1, TestId=1 },
                new StatusTest() { Id = 3, Status="Не пройден", AmountOfFaithfullAnswers=0, AmountOfIncorrectAnswers=0, Mark=null, StatusThemeId=1, TestId=1 },
                new StatusTest() { Id = 4, Status="Не пройден", AmountOfFaithfullAnswers=0, AmountOfIncorrectAnswers=0, Mark=null, StatusThemeId=1, TestId=1 },
            };
        }

        [Fact]
        public async Task ShouldStartTestAsync()
        {
            // Act
            var created = await service.StartTestAsync(1, 1);

            //Assert
            Assert.NotNull(created);
        }

        [Fact]
        public void ShouldCalculateTheMark()
        {
            // Arrange 
            int total = 10;
            int faithfull = 8;
            int incorrect = 2;

            // Act
            var result = service.CalculateTheMark(total, faithfull, incorrect);

            //Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public async Task ShouldFihishTheAttemptAsync()
        {
            // Act
            var finishTheAttempt = await service.FihishTheAttemptAsync(1);

            //Assert
            Assert.Null(finishTheAttempt);
        }
    }
}
