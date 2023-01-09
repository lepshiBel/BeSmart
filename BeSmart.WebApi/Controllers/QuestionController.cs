using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Question;
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
        public async Task<ActionResult<List<QuestionDTO>>> GetAll()
        {
            var questions = await serviceQuestion.GetAllQuestionsAsync();
            
            if (!questions.Any())
            {
                return NoContent();
            }

            return Ok(questions.OrderBy(a => a.Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> Get(int id)
        {
            var questionDto = await serviceQuestion.FindQuestionByIdAsync(id);

            if (questionDto is null)
            {
                return NoContent();
            }

            return Ok(questionDto);
        }

        [HttpGet("withAnswers/{id}")]
        public async Task<ActionResult<QuestionWithAnswersDTO>> GetQuestionWithAnswers(int id)
        {
            var questionsWithAnswersDto = await serviceQuestion.GetQuestionWithAnswersAsync(id);

            if (questionsWithAnswersDto is null)
            {
                return NoContent();
            }

            return Ok(questionsWithAnswersDto);
        }

        [HttpPost("Create/{testId}")]
        public async Task<ActionResult> Post(int testId, [FromBody]QuestionCreationDTO questionCreationDto)
        {
            questionCreationDto.TestId = testId;
            var createdQuestion = await serviceQuestion.AddQuestionAsync(questionCreationDto);
           
            if (createdQuestion is null)
            {
                return BadRequest("Question object is invalid");
            }

            return Ok(createdQuestion);
        }


        [HttpDelete("Delete/{id}")]
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