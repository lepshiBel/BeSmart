using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Test;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceTest
    {
        Task<List<TestDTO>> GetAllTestsAsync();
        Task<TestDTO> FindTestByIdAsync(int id);
        Task<TestWithQuestionsDTO> GetTestWithQuestionsAsync(int id);
        Task<TestDTO> AddTestAsync(TestCreationDTO testCreationDto);
        Task<TestDTO> UpdateTestAsync(int id, TestUpdateDTO testUpdateDto);
        Task<Test> DeleteTestAsync(int id);
    }
}
