using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;

namespace Subscriber.Services.Services
{
    public class CardService : ICardService
    {
        private ICardDal _cardDal;
        private IMapper _mapper;

        public CardService(ICardDal cardDal)
        {
            _cardDal = cardDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapCardModel>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<Card> CreateCard(float height, Guid mySubscriberID)
        {
            Card newCard = new Card
            {
              BMI = 0,
              Height = height,
              SubscriberID = mySubscriberID, 
              OpenDate = DateTime.Now 
            };
            return newCard;
        }

        public async Task<CardModel> GetCardInfoByID(int ID)
        {
            Card card = await _cardDal.GetCardByID(ID);
            if (card is null)
            {
                return null;
            }
            CardModel mycard = _mapper.Map<CardModel>(card);
            return mycard;

        }

        public async Task<int> GetMyCard(Guid subscriberID)
        {
            int cardID = await _cardDal.GetCard(subscriberID);
            return cardID;
        }
        public Task<bool> ExistCardId(int cardId)
        {
            return _cardDal.ExistCardId(cardId);
        }

        public Task<float> UpdateBMIAndWeight(int cardID, float weight)
        {
            return _cardDal.UpdateBMIAndWeight(cardID, weight);
        }

    }
}
