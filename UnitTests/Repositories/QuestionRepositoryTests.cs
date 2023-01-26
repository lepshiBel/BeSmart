using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Repositories;

public class QuestionRepositoryUnitTests
{
    private readonly IQuestionRepository _sut;
    private readonly BeSmartDbContext _context;

    public QuestionRepositoryUnitTests()
    {
        var options = new DbContextOptionsBuilder()
            .UseInMemoryDatabase("test")
            .Options;

        var context = new BeSmartDbContext(options);
        _context = context;
        _sut = new QuestionRepository(context);

        if (!_context.Questions.Any())
        {
            FillDatabase(context);
        }
    }
    public void FillDatabase(BeSmartDbContext context)
    {
        var data = new List<Question>()
        {
            new() {Id = 1, Text = "test",TestId = 1},
            new() {Id = 2, Text = "test2",TestId = 2},
        };

        context.Questions.AddRange(data);
        context.SaveChanges();
    }

    [Fact]
    public async Task ShouldAddQuestion()
    {
        // Arrange
        var data = new Question() { Id = 3, TestId = 1, Text = "asd" };

        // Act
        await _sut.AddAsync(data);

        // Assert
        var actual = _context.Questions.FirstOrDefault(e => e.Id == 3);
        Assert.NotNull(actual);
    }

    [Fact]
    public async Task ShouldDeleteQuestion()
    {
        // Act
        await _sut.DeleteAsync(1);

        // Assert
        var actual = _context.Questions.FirstOrDefault(e => e.Id == 1);
        Assert.Null(actual);
    }

    [Fact]
    public async Task ShouldGetQuestions()
    {
        // Act
        var data = await _sut.GetAsync(1);

        // Assert
        Assert.NotNull(data);
    }

    [Fact]
    public async Task ShouldNotGetQuestion()
    {
        // Act
        var data = await _sut.GetAsync(5);

        // Assert
        Assert.Null(data);
    }

    [Fact]
    public async Task ShouldUpdateQuestion()
    {
        var data = _context.Questions.FirstOrDefault(e => e.Id == 1);
        var newValue = "newTestValue";
        data.Text = newValue;

        // Act
        await _sut.UpdateAsync(data.Id, data);

        // Assert
        var actual = _context.Questions.First(e => e.Id == 1);
        Assert.Equal(actual.Text, newValue);
    }

    [Fact]
    public async Task ShouldReturnAll()
    {
        // Act
        var data = await _sut.GetAllAsync();

        // Assert
        Assert.Equal(2, data.Count);
    }
}