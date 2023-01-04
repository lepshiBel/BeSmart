using BeSmart.Application.Interfaces;
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
        public async Task<ActionResult<List<Test>>> GetAll()
        {
            var tests = await serviceTest.GetAllTestsAsync();

            if (!tests.Any())
            {
                return NoContent();
            }

            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> Get(int id)
        {
            var test = await serviceTest.FindTestByIdAsync(id);

            if (test is null)
            {
                return NoContent();
            }
            else
            {
                return Ok(test);
            }
        }

        [HttpGet("withQuestions/{id}")]
        public async Task<ActionResult<Test>> GetTestWithQuestions(int id)
        {
            var test = await serviceTest.GetTestWithQuestionsAsync(id);

            if (test is null)
            {
                return NoContent();
            }
            else
            {
                return Ok(test);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Test test)
        {
            var createdTest = await serviceTest.AddTestAsync(test);

            if (createdTest is null)
            {
                return BadRequest("Test object is invalid");
            }

            return Ok(createdTest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Test test)
        {
            if (id != test.Id)
            {
                return BadRequest();
            }

            var testToUpdate = await serviceTest.FindTestByIdAsync(id);

            if (testToUpdate is null)
            {
                return NoContent();
            }

            var updated = await serviceTest.UpdateTestAsync(testToUpdate);

            if (updated is null)
            {
                return BadRequest("Test object is invalid");
            }

            return Ok(updated);
        }

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
