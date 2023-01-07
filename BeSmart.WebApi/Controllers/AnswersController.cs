using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
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
        public async Task<ActionResult<List<AnswerDTO>>> GetAll()
        {
            var answersDto = await serviceAnswer.GetAllAnswersAsync();
            
            if (answersDto == null)
            {
                return NoContent();
            }

            return Ok(answersDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDTO>> Get(int id)
        {
            var answerDto = await serviceAnswer.FindAnswerByIdAsync(id);
            
            if (answerDto is null)
            {
                return NoContent();
            }

            return Ok(answerDto);
        }

        [HttpPost("Create/{answerDto}")]
        public async Task<ActionResult> Post(int questionId, AnswerCreationDTO answerDto)
        {
            answerDto.QuestionId = questionId;
            var createdAnswer = await serviceAnswer.AddAnswerAsync(answerDto);

            if (createdAnswer is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return Ok(createdAnswer);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<AnswerDTO>> Update(int id, AnswerUpdateDTO answerUpdateDTO)
        {
            var updated = await serviceAnswer.UpdateAnswerAsync(id, answerUpdateDTO);

            if (updated is null)
            {
                return NoContent();
            }

            return RedirectToAction("Get", "Answers", updated.Id);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceAnswer.DeleteAnswerAsync(id);
            
            if (entity == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

