using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;

namespace Subscriber.Services.Services
{
    public class SubscriberService : ISubscriberService
    {
        private ICardService _cardService;
        private ISubscriberDal _subscriberDal;
        private IMapper _mapper;

        public SubscriberService(ICardService cardService, ISubscriberDal subscriberDal)
        {
            _cardService = cardService;
            _subscriberDal = subscriberDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapCardModel>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<int> GetMyCard(string email, string password)
        {
            Guid subscriberID  =  await _subscriberDal.getSubscriberID(email, password);
            if (subscriberID.Equals(Guid.Empty))
            {
                return -1;
            }
            return await _cardService.GetMyCard(subscriberID);
        }

        public bool IsValid(string password)
        {
            return true;
        }

        public async Task<bool> Register(SubscriberModel subscriber, float height)
        {
            try
            {
                bool emailExists = await _subscriberDal.IsExistEmail(subscriber.Email);
                if (emailExists)
                {
                    return false;
                }
                Data.Entities.Subscriber mySubscriber = _mapper.Map<Data.Entities.Subscriber>(subscriber);
                mySubscriber.ID =Guid.NewGuid();
                Card card = await _cardService.CreateCard(height, mySubscriber.ID);
                bool result = await _subscriberDal.registerSubscriber(mySubscriber, card);
                return result;

            }
            catch
            {
                return false;
            }
        }

      
    }
}
