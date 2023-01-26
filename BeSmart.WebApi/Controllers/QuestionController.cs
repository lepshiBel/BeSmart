using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<QuestionDTO>> Update(int id, [FromBody] QuestionCreationDTO questionUpdateDTO)
        {
            var updated = await serviceQuestion.UpdateQuestionAsync(id, questionUpdateDTO);

            if (updated is null)
            {
                return BadRequest("Question object is invalid");
            }

            return Ok(updated);
        }

        [AllowAnonymous]
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