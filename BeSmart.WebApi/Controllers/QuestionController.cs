using AutoMapper;
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
        private readonly IMapper mapper;

        public QuestionController(IServiceQuestion serviceQuestion, IMapper mapper)
        {
            this.serviceQuestion = serviceQuestion;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionDTO>>> GetAll()
        {
            var questions = await serviceQuestion.GetAllQuestionsAsync();
            
            if (!questions.Any())
            {
                return NoContent();
            }

            return Ok(questions.Select(c => mapper.Map<QuestionDTO>(c)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> Get(int id)
        {
            var question = await serviceQuestion.FindQuestionByIdAsync(id);

            if (question is null)
            {
                return NoContent();
            }

            var questionDto = mapper.Map<QuestionDTO>(question);

            return Ok(questionDto);
        }

        [HttpGet("withAnswers/{id}")]
        public async Task<ActionResult<Question>> GetQuestionWithAnswers(int id)
        {
            var question = await serviceQuestion.GetQuestionWithAnswersAsync(id);

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
        public async Task<ActionResult> Post(QuestionCreationDTO questionCreationDto)
        {
            Question questionToCreate = mapper.Map<Question>(questionCreationDto);
            var createdQuestion = await serviceQuestion.AddQuestionAsync(questionToCreate);
           
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