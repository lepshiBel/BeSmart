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
        public async Task<ActionResult> Post(AnswerCreationDTO answerDto)
        {
            var createdAnswer = await serviceAnswer.AddAnswerAsync(answerDto);

            if (createdAnswer is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return RedirectToAction("Get", "Answers", createdAnswer.Id);
        }

        //[HttpPost("Update/{id}")]
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Answer>> Update(int id, AnswerCreationDTO answerCreationDTO)
        //{
        //    var answerToUpdate = await serviceAnswer.FindAnswerByIdAsync(id);
           
        //    if (answerToUpdate is null)
        //    {
        //        return NoContent();
        //    }

        //    var updated = await serviceAnswer.UpdateAnswerAsync(answerToUpdate, answerCreationDTO);

        //    return RedirectToAction("Get", "Answers", updated.Id);
        //}

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

