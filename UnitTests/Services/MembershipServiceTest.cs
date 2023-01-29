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
    public class MembershipServiceTest : IDisposable
    {
        private readonly IServiceMembership membershipService;
        private readonly IRepositoryManager manager;
        private readonly BeSmartDbContext context;

        public MembershipServiceTest()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("test")
               .Options;

            var dbContext = new BeSmartDbContext(options);
            this.context = dbContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            manager = new RepositoryManager(context);
            var configuration = new MapperConfiguration(conf =>
                     conf.AddMaps(typeof(AutoMapperProfile).Assembly));
            var mapper = new Mapper(configuration);

            membershipService = new MembershipService(manager, mapper);

            if (!context.Memberships.Any())
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
            var memberships = new List<Membership>()
            {
                new ()  { Id = 1, Status = "В процессе", AmountOfCompletedThemes = 0, CourseId = 1, UserId = 1},
                new ()  { Id = 2, Status = "Завершен", AmountOfCompletedThemes = 2, CourseId = 1, UserId = 3},
                new ()  { Id = 3, Status = "В процессе", AmountOfCompletedThemes = 1, CourseId = 1, UserId = 2},
            };

            var courses = new List<Course>()
            {
                new ()  { Id = 1, Name="course", CountOfThemes=0, CreatedById=1, CategoryId=3},
                new ()  { Id = 2,  Name="course", CountOfThemes=0, CreatedById=1, CategoryId=4},
            };


            context.Memberships.AddRange(memberships);
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldReturnAllMemberships()
        {
            // Act
            var data = await membershipService.GetAllMembershipsAsync();

            // Assert
            Assert.Equal(3, data.Count);
        }

        [Fact]
        public async Task ShouldReturnAllMembershipsForUser()
        {
            // Act
            var data = await membershipService.GetAllMembershipsForUserAsync(1);

            // Assert
            Assert.Equal(1, data.Count);
        }

        [Fact]
        public async Task ShouldReturnMembershipWithThemes()
        {
            // Act
            var data = await membershipService.GetMembershipWithThemesForUserAsync(1);

            // Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldNotReturnMembershipsWithThemes()
        {
            // Act
            var data = await membershipService.GetMembershipWithThemesForUserAsync(6);

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public void ShouldCheckIfMembershipExists()
        {
            // Act
            var data = membershipService.CheckMembershipAsync(1, 1);

            // Assert
            Assert.True(data);
        }

        [Fact]
        public void ShouldCheckIfMembershipDoesntExist()
        {
            // Act
            var data = membershipService.CheckMembershipAsync(4, 1);

            // Assert
            Assert.False(data);
        }

        [Fact]
        public async Task ShouldDeleteMembership()
        {
            // Act
            await membershipService.DeleteMembershipAsync(1);

            // Assert
            var actual = context.Memberships.FirstOrDefault(e => e.Id == 1);
            Assert.Null(actual);
        }

        [Fact]
        public async Task ShouldAddMembership()
        {
            // Act
            await membershipService.CreateNewMembershipAsync(2, 1);

            // Assert
            var actual = context.Memberships.FirstOrDefault(e => e.Id == 4);
            Assert.NotNull(actual);
        }
    }
    


}

//Task<List<Membership>> GetAllMembershipsAsync();
//Task<List<MembershipDTO>> GetAllMembershipsForUserAsync(int userId);
//Task<MembershipWithThemesDTO> GetMembershipWithThemesForUserAsync(int membershipId);

//Task<Membership> CreateNewMembershipAsync(int courseId, int userId);
//Task<Membership> DeleteMembershipAsync(int id);