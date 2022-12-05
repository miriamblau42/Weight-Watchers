using Microsoft.EntityFrameworkCore;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;

namespace Subscriber.Data
{
    public class SubscriberDal : ISubscriberDal,ICardDal
    {
        private SubscribeDBContext _subscriberContext;

        public SubscriberDal(SubscribeDBContext subscriberContext)
        {
            _subscriberContext = subscriberContext;
        }

        public async Task AddCard(Card card)
        {
            try
            {
                await _subscriberContext.Cards.AddAsync(card);
            }
            catch
            {
                throw new NotImplementedException();
            }; ;
        }
        public async Task AddSubscribe(Entities.Subscriber subscriber)
        {
            try
            {
                await _subscriberContext.Subscribers.AddAsync(subscriber);
            }
            catch
            {
                throw new NotImplementedException();
            }; 
        }

        public async Task<bool> DeleteSubscriber(Entities.Subscriber subscriber)
        {
            try
            {
                 _subscriberContext.Subscribers.Remove(subscriber);
                await _subscriberContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetCard(Guid subscriberID)
        {
            try
            {
                var card = await _subscriberContext.Cards.FirstOrDefaultAsync(card => card.SubscriberID.Equals(subscriberID));
                if (card != null)
                {
                    return card.ID;
                }
                return -1;
            }
            catch
            {

                return -1;
            }
        }

        public async Task<Card> GetCardByID(int ID)
        {
            try
            {
                var card = await _subscriberContext.Cards.Include(c => c.Subscriber).FirstOrDefaultAsync(card => card.ID == ID);
                return card;
            }
            catch
            {
                //in card will be null...
                return null;
            }
        }

        public async Task<Guid> getSubscriberID(string email, string password)
        {
            var subscriber=await _subscriberContext.Subscribers.FirstOrDefaultAsync(s=>s.Email==email&& s.Password==password);
            if (subscriber == null)
            {
                return Guid.Empty;
            }
            return subscriber.ID;
        }

        public async Task<bool> IsExistEmail(string email)
        {
            var isExisted =   _subscriberContext.Subscribers
                .Any(s => s.Email.Equals(email));
            return isExisted? true:false;
        }

        public async Task<bool> registerSubscriber(Entities.Subscriber subscriber,Card card)
        {
            try
            {

                await AddSubscribe(subscriber);
                await AddCard(card);

                try
                {
                    
                    await _subscriberContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ExistCardId(int cardID)
        {
            return await _subscriberContext.Cards.AnyAsync(card => card.ID == cardID);
        }
        public async Task<float> UpdateBMIAndWeight(int cardID, float weight)
        {
            try
            {

                Card card = await _subscriberContext.Cards.FirstAsync(c => c.ID == cardID);
                card.BMI = weight / (card.Height * card.Height);
                card.Weight = weight;
                await _subscriberContext.SaveChangesAsync();
                return card.BMI;
            }
            catch
            {
                return 0;
            }
        }
    }
}

