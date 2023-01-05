using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IServiceAnswer serviceAnswer;
        private readonly IMapper mapper;

        public AnswersController(IServiceAnswer serviceAnswer, IMapper mapper)
        {
            this.serviceAnswer = serviceAnswer;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Answer>>> GetAll()
        {
            var answers = await serviceAnswer.GetAllAnswersAsync();
            
            if (!answers.Any())
            {
                return NoContent();
            }

            return Ok(answers.Select(x => mapper.Map<AnswerDTO>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Get(int id)
        {
            var answer = await serviceAnswer.FindAnswerByIdAsync(id);
            
            if (answer is null)
            {
                return NoContent();
            }

            return Ok(mapper.Map<AnswerDTO>(answer));
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> Post(AnswerCreationDTO answerdto)
        {
            var answerToAdd = mapper.Map<Answer>(answerdto);
            var createdAnswer = await serviceAnswer.AddAnswerAsync(answerToAdd);

            if (createdAnswer is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return Ok(createdAnswer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Answer>> Update(int id)
        {
            var answerToUpdate = await serviceAnswer.FindAnswerByIdAsync(id);
           
            if (answerToUpdate is null)
            {
                return NoContent();
            }

            var updated = await serviceAnswer.UpdateAnswerAsync(answerToUpdate);
            
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceAnswer.DeleteAnswerAsync(id);
            
            if (entity == null)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}

