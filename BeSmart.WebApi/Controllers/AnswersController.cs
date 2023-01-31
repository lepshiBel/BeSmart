using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles ="user")]
        [HttpPost("GiveTheAnswerToTheQuestion/{answerId}, {statusTestId}")]
        public async Task<ActionResult> GiveTheAnswerToTheQuestion(int answerId, int statusTestId)
        {
            var passedAnswer = await serviceAnswer.FindAnswerByIdAsync(answerId);

            if (passedAnswer == null) return BadRequest("Passed answerId is invalid");

            var updatedStatusTest = await serviceAnswer.CheckAnswerAndUpdateStatusTest(passedAnswer.Fidelity, statusTestId);

            if (updatedStatusTest == null) return BadRequest("Passed statusTestId is invalid");

            return Ok(updatedStatusTest);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AnswerDTO>>> GetAll()
        {
            var answersDto = await serviceAnswer.GetAllAnswersAsync();
            
            if (answersDto == null)
            {
                return NoContent();
            }

            return Ok(answersDto.OrderBy(a => a.Id));
        }
     
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("Create/{questionId}")]
        public async Task<ActionResult> Post(int questionId, [FromBody]AnswerCreationDTO answerDto)
        {
            answerDto.QuestionId = questionId;
            var createdAnswer = await serviceAnswer.AddAnswerAsync(answerDto);

            if (createdAnswer is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return Ok(createdAnswer);
        }

        [AllowAnonymous]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<AnswerDTO>> Update(int id, [FromBody]AnswerUpdateDTO answerUpdateDTO)
        {
            var updated = await serviceAnswer.UpdateAnswerAsync(id, answerUpdateDTO);

            if (updated is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return Ok(updated);
        }

        [AllowAnonymous]
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

