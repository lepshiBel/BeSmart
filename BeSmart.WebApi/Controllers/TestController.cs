using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs;
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

            return Ok(testsDto);
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

        [HttpPost("Create/{testCreationDto}")]
        public async Task<ActionResult<Test>> Post(TestCreationDTO testCreationDto)
        {
            var createdTest = await serviceTest.AddTestAsync(testCreationDto);

            if (createdTest is null)
            {
                return BadRequest("Test object is invalid");
            }

            return RedirectToAction("Get", "Test", createdTest.Id);
        }

        //[HttpPut("Update/{id}")]
        //public async Task<ActionResult> Update(int id, Test test)
        //{
        //    if (id != test.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var testToUpdate = await serviceTest.FindTestByIdAsync(id);

        //    if (testToUpdate is null)
        //    {
        //        return NoContent();
        //    }

        //    var updated = await serviceTest.UpdateTestAsync(testToUpdate);

        //    if (updated is null)
        //    {
        //        return BadRequest("Test object is invalid");
        //    }

        //    return Ok(updated);
        //}

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
