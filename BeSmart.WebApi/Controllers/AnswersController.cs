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
            try
            {
                var answers = await serviceAnswer.GetAllAsync();
                if(!answers.Any())
                {
                    return NotFound();
                }
                return Ok(answers);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }         
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Get(int id)
        {
            try
            {
                var answer = await serviceAnswer.FindByIdAsync(id);
                if (answer is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(answer);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> Post(Answer answer)
        {
            try
            {
                if (answer is null)
                {
                    return BadRequest("Answer object is null");
                }
                // добавить валидацию
                var createdAnswer = await serviceAnswer.AddAsync(answer);
                return CreatedAtRoute("OwnerById", new { id = createdAnswer.Id }, createdAnswer);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Answer>> Update(int id)
        {
            try
            {
                var answerToUpdate = await serviceAnswer.FindByIdAsync(id);
                if (answerToUpdate is null)
                {
                    return NotFound();
                }
                // добавить валидацию
                var updated = await serviceAnswer.UpdateAsync(answerToUpdate);
                return Ok(updated);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> Delete(int id)
        {
            try
            {
                var entity = await serviceAnswer.DeleteAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

