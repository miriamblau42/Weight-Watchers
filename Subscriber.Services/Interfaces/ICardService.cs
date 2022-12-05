using Subscriber.Data.Entities;
using Subscriber.Services.Models;

namespace Subscriber.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card> CreateCard(float height, Guid mySubscriberID);
        Task<int> GetMyCard(Guid subscriberID);
        Task<CardModel> GetCardInfoByID(int ID);
        Task<bool> ExistCardId(int cardId);
        Task<float> UpdateBMIAndWeight(int cardID, float weight);
    }
}
