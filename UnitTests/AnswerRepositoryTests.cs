using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests;

public class AnswerRepositoryUnitTests
{
    private readonly IAnswerRepository _sut;
    private readonly BeSmartDbContext _context;
    
    public AnswerRepositoryUnitTests()
    {
        var options = new DbContextOptionsBuilder()
            .UseInMemoryDatabase("test")
            .Options;
        
        var context = new BeSmartDbContext(options);
        _context = context;
        _sut = new AnswerRepository(context);

        if (!_context.Answers.Any())
        {
            FillDatabase(context);
        }
    }

    public void FillDatabase(BeSmartDbContext context)
    {
        var data = new List<Answer>()
        {
            new() {Id = 1, Text = "test", QuestionId = 1, Fidelity = true},
            new() {Id = 2, Text = "test2", QuestionId = 2, Fidelity = true},
        };
        
        context.Answers.AddRange(data);
        context.SaveChanges();
    }

    
    [Fact]
    public async Task ShouldAddAnswer()
    {
        // Arrange
        var data = new Answer() {Id = 3, Fidelity = true, QuestionId = 1, Text = "asd"};
        
        // Act
        await _sut.AddAsync(data);

        // Assert
        var actual = _context.Answers.FirstOrDefault(e => e.Id == 3);
        Assert.NotNull(actual);
    }

    [Fact]
    public async Task ShouldDeleteAnswer()
    {
        // Act
        await _sut.DeleteAsync(1);

        // Assert
        var actual = _context.Answers.FirstOrDefault(e => e.Id == 1);
        Assert.Null(actual);
    }

    [Fact]
    public async Task ShouldGetAnswer()
    {
        // Act
        var data = await _sut.GetAsync(1);
        
        // Assert
        Assert.NotNull(data);
    }
    
    [Fact]
    public async Task ShouldNotGetAnswer()
    {
        // Act
        var data = await _sut.GetAsync(5);
        
        // Assert
        Assert.Null(data);
    }

    [Fact]
    public async Task ShouldUpdateAnswer()
    {
        var data = _context.Answers.FirstOrDefault(e => e.Id == 1);
        var oldValue = data.Text;
        var newValue = "newTestValue";
        data.Text = newValue;
        
        // Act
        await _sut.UpdateAsync(data.Id, data);

        // Assert
        var actual = _context.Answers.First(e => e.Id == 1);
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