using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Interfaces
{
    public interface ICardDal
    {
        Task AddCard(Card card);
        Task<int> GetCard(Guid subscriberID);
        Task<Card> GetCardByID(int ID);
        Task<bool> ExistCardId(int cardId);
        Task<float> UpdateBMIAndWeight(int cardID, float weight);
    }
}
