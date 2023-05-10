using Microsoft.Extensions.Configuration;
using Subscriber.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Subscriber.Data;

public class SubscriberData : ISubscriberData
{
    private IDbContextFactory<SubscriberContext> _factory;

    public SubscriberData(IDbContextFactory<SubscriberContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    }

    public async Task<bool> SubscriberExists(string email)
    {
        try
        {
            using var db = _factory.CreateDbContext();

            return await db.Subscribers.AnyAsync(s => s.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }
    public async Task AddNewSubscriber(Entities.Subscriber newSubscriber)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            var aa = db.Subscribers.ToList();
            db.Database.SetCommandTimeout(6000);
            await db.Subscribers.AddAsync(newSubscriber);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }
    public async Task<int> Login(string email, string password)
    {
        try
        {
            Card cardFound;
            using var db = _factory.CreateDbContext();
            Entities.Subscriber subscriberFound = await db.Subscribers.FirstOrDefaultAsync(s =>
            s.Email.Equals(email) && s.Password.Equals(password));
            if (subscriberFound == null) return -1;
            cardFound = await db.Cards.FirstOrDefaultAsync(c => c.SubscriberId == subscriberFound.Id);

            if (cardFound == null) return -1;
            return cardFound.Id;

        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }

    public async Task<bool> AddNewCard(Card newCard)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            newCard.BMI = calcBMI(newCard.Height, newCard.Weight);
            await db.Cards.AddAsync(newCard);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }

    public async Task<Card> getCardById(int id)
    {

        try
        {
            using var db = _factory.CreateDbContext();
            return db.Cards.Where(c => c.Id == id).Include(c => c.Subscriber).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }

    public async Task<bool> CardExists(int cardId)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            return await db.Cards.AnyAsync(c => c.Id == cardId);
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to read data from db", ex);
        }
    }

    public async Task<Card> UpdateBMI(int cardId, float weight)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            var card = await db.Cards.FirstOrDefaultAsync(c => c.Id == cardId);
            if (card != null)
            {
                card.Weight = weight;
                card.BMI = calcBMI(card.Height, card.Weight);
                db.SaveChanges();
            }
            return card;
        }
        catch (Exception ex)
        {
            throw new Exception("Server error when trying to access data in db", ex);
        }
    }

    private float calcBMI(float height, float weight)
    {
        height /= 100;
        return weight / (height * height);
    }
}
