
using Subscriber.Data.Entities;

namespace Subscriber.Data.Interfaces
{
    public interface ISubscriberDal
    {
        Task<bool> registerSubscriber(Entities.Subscriber subscriber,Card card);
        Task<bool> IsExistEmail(string email);
        Task<bool> DeleteSubscriber(Entities.Subscriber subscriber);
        Task<Guid> getSubscriberID(string email, string password);
        Task AddSubscribe(Entities.Subscriber subscriber);
    }
}