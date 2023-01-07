using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceCard
    {
        Task<List<CardDTO>> GetAllCardsAsync();
        Task<CardDTO> FindCardByIdAsync(int id);
        Task<CardDTO> AddCardAsync(CardCreationDTO card);
        Task<CardDTO> UpdateCardAsync(int id, CardUpdateDTO card);
        Task<Card> DeleteCardAsync(int id);
    }
}
