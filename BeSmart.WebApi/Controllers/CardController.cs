using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Card;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IServiceCard serviceCard;

        public CardController(IServiceCard serviceCard)
        {
            this.serviceCard = serviceCard;
        }

        [HttpGet]
        public async Task<ActionResult<List<CardDTO>>> GetAll()
        {
            var cardsDto = await serviceCard.GetAllCardsAsync();

            if (cardsDto == null)
            {
                return NoContent();
            }

            return Ok(cardsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardDTO>> Get(int id)
        {
            var cardDto = await serviceCard.FindCardByIdAsync(id);

            if (cardDto is null)
            {
                return NoContent();
            }

            return Ok(cardDto);
        }

        [HttpPost("Create/{lessonId}")]
        public async Task<ActionResult> Post(int lessonId, [FromBody]CardCreationDTO cardDto)
        {
            cardDto.LessonId = lessonId;
            var createdCard = await serviceCard.AddCardAsync(cardDto);

            if (createdCard is null)
            {
                return BadRequest("Answer object is invalid");
            }

            return Ok(createdCard);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CardDTO>> Update(int id, [FromBody]CardUpdateDTO cardUpdateDTO)
        {
            var updated = await serviceCard.UpdateCardAsync(id, cardUpdateDTO);

            if (updated is null)
            {
                return NoContent();
            }

            return RedirectToAction("Get", "Cards", updated.Id);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceCard.DeleteCardAsync(id);

            if (entity == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
