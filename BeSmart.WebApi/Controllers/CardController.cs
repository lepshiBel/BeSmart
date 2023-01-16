﻿using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CardDTO>>> GetAll()
        {
            var cardsDto = await serviceCard.GetAllCardsAsync();

            if (cardsDto == null)
            {
                return NoContent();
            }

            return Ok(cardsDto.OrderBy(a => a.Id));
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("Create/{lessonId}")]
        public async Task<ActionResult> Post(int lessonId, [FromBody]CardCreationDTO cardDto)
        {
            cardDto.LessonId = lessonId;
            var createdCard = await serviceCard.AddCardAsync(cardDto);

            if (createdCard is null)
            {
                return BadRequest("Card object is invalid");
            }

            return Ok(createdCard);
        }

        [AllowAnonymous]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CardDTO>> Update(int id, [FromBody]CardUpdateDTO cardUpdateDTO)
        {
            var updated = await serviceCard.UpdateCardAsync(id, cardUpdateDTO);

            if (updated is null)
            {
                return BadRequest("Card object is invalid");
            }

            return RedirectToAction("Get", "Cards", updated.Id);
        }

        [AllowAnonymous]
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

        [HttpGet(nameof(GetRole))]
        [Authorize(Roles = "user, admin")]
        public ActionResult GetRole()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return Ok(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value);
            }
            return BadRequest();
        }
    }
}
