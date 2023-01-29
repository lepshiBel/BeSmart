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
        private readonly ICacheService _cacheService;

        public CardService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(CardService), dataId);
        }

        public async Task<List<CardDTO>>? GetAllCardsAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<CardDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var cards = await repoManager.Card.GetAllAsync();
            var mappedResult = cards == null ? null : mapper.Map<List<CardDTO>>(cards);

            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CardDTO>? FindCardByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<CardDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var card = await repoManager.Card.GetAsync(id);
            var mappedResult = card == null ? null : mapper.Map<CardDTO>(card);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
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
            var mappedResult = updated == null ? null : mapper.Map<CardDTO>(updated);

            var cacheKey = GetCacheKey(id.ToString());
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<Card> DeleteCardAsync(int id)
        {
            var result = await repoManager.Card.DeleteAsync(id);

            var cacheKey = GetCacheKey(id.ToString());
            await _cacheService.DeleteCachedData(cacheKey);
            return result;
        }
    }
}
