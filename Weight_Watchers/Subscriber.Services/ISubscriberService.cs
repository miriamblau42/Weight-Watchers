using Subscriber.Services.Models;

namespace Subscriber.Services
{
    public interface ISubscriberService
    {
        public Task<bool> AddNewSubscriber(SubscriberModel newSubscriberModel,float hight);
        public Task<int> Login(string email, string password);
        Task<CardModel> getCardById(int id);
    }
}
