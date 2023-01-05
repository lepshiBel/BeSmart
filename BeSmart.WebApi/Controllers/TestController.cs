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
        private readonly IMapper mapper;

        public TestController(IServiceTest serviceTest, IMapper mapper)
        {
            this.serviceTest = serviceTest;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestDTO>>> GetAll()
        {
            var tests = await serviceTest.GetAllTestsAsync();

            if (!tests.Any())
            {
                return NoContent();
            }

            return Ok(tests.Select(t => mapper.Map<TestDTO>(t)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDTO>> Get(int id)
        {
            var test = await serviceTest.FindTestByIdAsync(id);

            if (test is null)
            {
                return NoContent();
            }
            
            var testDto = mapper.Map<TestDTO>(test);
            
            return Ok(testDto);
        }

        [HttpGet("withQuestions/{id}")]
        public async Task<ActionResult<TestWithQuestionsDTO>> GetTestWithQuestions(int id)
        {
            var test = await serviceTest.GetTestWithQuestionsAsync(id);

            if (test is null)
            {
                return NoContent();
            }

            var testWithAnswersDto = mapper.Map<TestWithQuestionsDTO>(test);

            return Ok(testWithAnswersDto);
        }

        [HttpPost]
        public async Task<ActionResult<Test>> Post(TestDTO testDto)
        {
            var testToAdd = mapper.Map<Test>(testDto);
            var createdTest = await serviceTest.AddTestAsync(testToAdd);

            if (createdTest is null)
            {
                return BadRequest("Test object is invalid");
            }

            return Ok(createdTest);
        }

        //[HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
