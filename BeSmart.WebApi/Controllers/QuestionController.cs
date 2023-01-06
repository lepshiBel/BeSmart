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

            return Ok(questions);
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

        [HttpPost("Create/{questionCreationDto}")]
        public async Task<ActionResult> Post(QuestionCreationDTO questionCreationDto)
        {
            var createdQuestion = await serviceQuestion.AddQuestionAsync(questionCreationDto);
           
            if (createdQuestion is null)
            {
                return BadRequest("Question object is invalid");
            }

            return RedirectToAction("Get", "Answers", createdQuestion.Id);
        }

        //[HttpPut("Update/{id}")]
        //public async Task<ActionResult> Update(int id, Question question)
        //{
        //    if (id != question.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var questionToUpdate = await serviceQuestion.FindQuestionByIdAsync(id);

        //    if (questionToUpdate is null)
        //    {
        //        return NoContent();
        //    }

        //    var updated = await serviceQuestion.UpdateQuestionAsync(questionToUpdate);

        //    if(updated is null)
        //    {
        //        return BadRequest("Question object is invalid");
        //    }

        //    return Ok(updated);
        //}

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