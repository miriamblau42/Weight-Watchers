
using Subscriber.Services.Models;

namespace Subscriber.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<bool> Register(SubscriberModel subscriber, float height);
        bool IsValid(string password);
        Task<int> GetMyCard(string email, string password);
    }
}
