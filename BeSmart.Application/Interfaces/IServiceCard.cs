using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceCard
    {
        Task<List<Card>> GetAllCardsAsync();
        Task<Card> FindCardByIdAsync(int id);
        Task<Card> AddCardAsync(Card card);
        Task<Card> UpdateCardAsync(int id, Card card);
        Task<Card> DeleteCardAsync(int id);
    }
}
