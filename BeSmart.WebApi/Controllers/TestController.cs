using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Test;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IServiceTest serviceTest;

        public TestController(IServiceTest serviceTest)
        {
            this.serviceTest = serviceTest;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestDTO>>> GetAll()
        {
            var testsDto = await serviceTest.GetAllTestsAsync();

            if (!testsDto.Any())
            {
                return NoContent();
            }

            return Ok(testsDto.OrderBy(a => a.Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDTO>> Get(int id)
        {
            var testDto = await serviceTest.FindTestByIdAsync(id);

            if (testDto is null)
            {
                return NoContent();
            }
            
            return Ok(testDto);
        }

        [HttpGet("withQuestions/{id}")]
        public async Task<ActionResult<TestWithQuestionsDTO>> GetTestWithQuestions(int id)
        {
            var testWithQuestionsDto = await serviceTest.GetTestWithQuestionsAsync(id);

            if (testWithQuestionsDto is null)
            {
                return NoContent();
            }

            return Ok(testWithQuestionsDto);
        }

        [HttpPost("Create/{themeId}")]
        public async Task<ActionResult<Test>> Post(int themeId, TestCreationDTO testCreationDto)
        {
            testCreationDto.ThemeId = themeId;
            var createdTest = await serviceTest.AddTestAsync(testCreationDto);

            if (createdTest is null)
            {
                return BadRequest("Test object is invalid");
            }

            return Ok(createdTest);
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult<Test>> Update(int id, TestUpdateDTO testUpdateDto)
        {
            var updatedTest = await serviceTest.UpdateTestAsync(id, testUpdateDto);

            if (updatedTest is null)
            {
                return BadRequest("Test object is invalid");
            }

            return Ok(updatedTest);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceTest.DeleteTestAsync(id);

            if (entity == null)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
