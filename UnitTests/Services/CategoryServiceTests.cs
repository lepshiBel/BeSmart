using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Services
{
    public class CategoryServiceTests
    {
        private readonly BeSmartDbContext context;
        private readonly IServiceCategory service;
        private readonly IRepositoryManager repoManager;

        public CategoryServiceTests()
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

            service = new CategoryService(repoManager, mapper);

            if (!context.Answers.Any())
            {
                SeedData(context);
            }

        }
        public void SeedData(BeSmartDbContext context)
        {
            var data = new List<Category>()
                {
                    new() {Id = 1, Name = "Test"},
                    new() {Id = 2, Name = "test2"},
                    new() {Id = 3, Name = "test3"},
                };

            context.Categories.AddRange(data);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldAddCategoryAsync()
        {
            // Arrange
            var data = new CategoryCreationDTO() { Name = "test4" };

            // Act
            await service.AddCategoryAsync(data);

            // Assert
            var actual = context.Categories.FirstOrDefault(c => c.Id == 4);

            Assert.NotNull(actual);

        }

        [Fact]
        public async Task ShouldGetCategorAsync()
        {
            // Act
            var data = await service.FindCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldGetAllCategoriesAsync()
        {
            // Act
            var data = await service.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotGetCategoryAsync()
        {
            // Act
            var data = await service.FindCategoryByIdAsync(10);

            // Assert
            Assert.Null(data);
        }
    }
}
