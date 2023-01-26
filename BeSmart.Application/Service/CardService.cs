using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class CardService : IServiceCard
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public CardService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<CardDTO>>? GetAllCardsAsync()
        {
            var cards = await repoManager.Card.GetAllAsync();
            return cards == null ? null : mapper.Map<List<CardDTO>>(cards);
        }

        public async Task<CardDTO>? FindCardByIdAsync(int id)
        {
            var card = await repoManager.Card.GetAsync(id);
            return card == null ? null : mapper.Map<CardDTO>(card);
        }

        public async Task<CardDTO> AddCardAsync(CardCreationDTO cardDto)
        {
            var cardToCreate = mapper.Map<Card>(cardDto);
            var createdCard = await repoManager.Card.AddAsync(cardToCreate);
            return mapper.Map<CardDTO>(createdCard);
        }

        public async Task<CardDTO> UpdateCardAsync(int id, CardUpdateDTO cardUpdateDto)
        {
            var cardToUpdate = mapper.Map<Card>(cardUpdateDto);
            var updated = await repoManager.Card.UpdateAsync(id, cardToUpdate);
            return updated == null ? null : mapper.Map<CardDTO>(updated);
        }

        public async Task<Card> DeleteCardAsync(int id)
        {
            return await repoManager.Card.DeleteAsync(id);
        }
    }
}
