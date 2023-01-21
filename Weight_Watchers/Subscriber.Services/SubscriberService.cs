using AutoMapper;
using Subscriber.Services.Models;
using Subscriber.Data.Entities;
using Subscriber.Data;
using System.Transactions;

namespace Subscriber.Services
{
    public class SubscriberService: ISubscriberService
    {
        private readonly ISubscriberData _subscriberData;
        private readonly IMapper _mapper;
        public SubscriberService(ISubscriberData subscriberData, IMapper mapper)
        {
            _subscriberData = subscriberData;
            _mapper = mapper;     
        }
        public async Task<bool> AddNewSubscriber(SubscriberModel newSubscriberModel, float hight)
        {
            bool succes = false;
            using (TransactionScope scope = new TransactionScope())
            {
                if (await _subscriberData.SubscriberExists(newSubscriberModel.Email)) return false;
                Data.Entities.Subscriber newSubscriber = _mapper.Map<Data.Entities.Subscriber>(newSubscriberModel);
                newSubscriber.Id = Guid.NewGuid().ToString();
                await _subscriberData.AddNewSubscriber(newSubscriber);
                Card newCard = new Card()
                {
                    OpenDate = DateTime.Now,
                    Height = hight,
                    SubscriberId = newSubscriber.Id

                };
               succes = await _subscriberData.AddNewCard(newCard);
               scope.Complete();

            }
            return succes;
        }
        public async Task<int> Login(string email, string password)
        {
            return await _subscriberData.Login(email, password);
        }

        public async Task<CardModel> getCardById(int id)
        {
            return _mapper.Map<CardModel>(await _subscriberData.getCardById(id));
        }

    }
}
