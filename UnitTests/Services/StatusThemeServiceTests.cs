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
    public class StatusThemeServiceTests : IDisposable
    {
        private readonly BeSmartDbContext context;
        private readonly IRepositoryManager manager;
        private readonly IServiceStatusTheme service;

        public StatusThemeServiceTests()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("test")
               .Options;
            context = new BeSmartDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            manager = new RepositoryManager(context);

            manager = new RepositoryManager(context);
            var configuration = new MapperConfiguration(conf =>
                     conf.AddMaps(typeof(AutoMapperProfile).Assembly));
            var mapper = new Mapper(configuration);


            service = new StatusThemeService(manager, mapper);

            if (!context.Memberships.Any())
            {
                SeedData(context);
            }
        }

        private void SeedData(BeSmartDbContext context)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
  
    }
}


//Task<StatusTheme> StartNewThemeAsync(StatusTheme existed);
//Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId);
//Task<StatusThemeWithLessonsDTO> GetStatusThemeWithStatusLessons(int statusThemeId);
