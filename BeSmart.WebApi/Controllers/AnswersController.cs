using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IServiceAnswer serviceAnswer;

        public AnswersController(IServiceAnswer serviceAnswer)
        {
            this.serviceAnswer = serviceAnswer;
        }

        [HttpGet]
        public async Task<ActionResult<List<Answer>>> GetAll()
        {
            var answers = await serviceAnswer.GetAllAnswersAsync();
            if (!answers.Any())
            {
                return NotFound();
            }
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Get(int id)
        {
            var answer = await serviceAnswer.FindAnswerByIdAsync(id);
            if (answer is null)
            {
                return NotFound();
            }

            return Ok(answer);
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> Post(Answer answer)
        {
            if (answer is null)
            {
                return BadRequest("Answer object is null");
            }
            // добавить валидацию
            var createdAnswer = await serviceAnswer.AddAnswerAsync(answer);
            return CreatedAtRoute("OwnerById", new { id = createdAnswer.Id }, createdAnswer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Answer>> Update(int id)
        {
            var answerToUpdate = await serviceAnswer.FindAnswerByIdAsync(id);
            if (answerToUpdate is null)
            {
                return NotFound();
            }
            // добавить валидацию
            var updated = await serviceAnswer.UpdateAnswerAsync(answerToUpdate);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceAnswer.DeleteAnswerAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

