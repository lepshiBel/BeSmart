using BeSmart.Application.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IServiceQuestion serviceQuestion;

        public QuestionController(IServiceQuestion serviceQuestion)
        {
            this.serviceQuestion = serviceQuestion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetAll()
        {
            try
            {
                var questions = await serviceQuestion.GetAllQuestionsAsync();
                if (!questions.Any())
                {
                    return NotFound();
                }
                return Ok(questions);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            try
            {
                var question = await serviceQuestion.FindQuestionByIdAsync(id);
                if (question is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(question);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Question>> Post(Question question)
        {
            try
            {
                if (question is null)
                {
                    return BadRequest("Question object is null");
                }
                // добавить валидацию
                var createdQuestion = await serviceQuestion.AddQuestionAsync(question);
                return CreatedAtRoute("OwnerById", new { id = createdQuestion.Id }, createdQuestion);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Question>> Update(int id)
        {
            try
            {
                var questionToUpdate = await serviceQuestion.FindQuestionByIdAsync(id);
                if (questionToUpdate is null)
                {
                    return NotFound();
                }
                // добавить валидацию
                var updated = await serviceQuestion.UpdateQuestionAsync(questionToUpdate);
                return Ok(updated);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> Delete(int id)
        {
            try
            {
                var entity = await serviceQuestion.DeleteQuestionAsync(id);
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