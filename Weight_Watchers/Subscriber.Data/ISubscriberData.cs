using Subscriber.Data.Entities;

namespace Subscriber.Data
{
    public interface ISubscriberData
    {
        Task<bool> AddNewCard(Card newCard);
        Task AddNewSubscriber(Entities.Subscriber newSubscriber);
        Task<bool> CardExists(int cardId);
        Task<Card> getCardById(int id);
        Task<int> Login(string email, string password);
        Task<bool> SubscriberExists(string email);
        Task<Card> UpdateBMI(int cardId, float weight);
    }
}