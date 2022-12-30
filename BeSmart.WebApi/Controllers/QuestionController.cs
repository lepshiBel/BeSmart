using BeSmart.Application.Interfaces;
using BeSmart.Domain.Models;
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
            var questions = await serviceQuestion.GetAllQuestionsAsync();
            if (!questions.Any())
            {
                return NoContent();
            }
            return Ok(questions);          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            var question = await serviceQuestion.FindQuestionByIdAsync(id);
            if (question is null)
            {
                return NoContent();
            }
            else
            {
                return Ok(question);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Question question)
        {
            var createdQuestion = await serviceQuestion.AddQuestionAsync(question);
            if (createdQuestion is null)
            {
                return BadRequest("Question object is invalid");
            }
            return Ok(createdQuestion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            var questionToUpdate = await serviceQuestion.FindQuestionByIdAsync(id);
            if (questionToUpdate is null)
            {
                return NoContent();
            }

            var updated = await serviceQuestion.UpdateQuestionAsync(questionToUpdate);
            if(updated is null)
            {
                return BadRequest("Question object is invalid");
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceQuestion.DeleteQuestionAsync(id);
            if (entity == null)
            {
                return NoContent();
            }
            return Ok();
        }
    }
}