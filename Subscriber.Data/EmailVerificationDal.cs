using Microsoft.EntityFrameworkCore;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data
{
    public class EmailVerificationDal:IEmailVerificationDal
    {
        private readonly SubscribeDBContext _context;
        public EmailVerificationDal(SubscribeDBContext contextFactory)
        {
            _context = contextFactory;
        }
        public async Task AddEmailVerification(EmailVerification emailVerification)
        {
            try
            {
                await _context.EmailVerifications.AddAsync(emailVerification);
                await _context.SaveChangesAsync();
            }
            catch
            {

            }
        }
        public async Task UpdateEmailVerification(EmailVerification emailVerification)
        {
            try
            {
                _context.EmailVerifications.Update(emailVerification);
                await _context.SaveChangesAsync();
            }
            catch
            {

            }
        }
        public async Task<bool> CheckEmailVerifiedByEmail(string email)
        {
            EmailVerification? emailVerification;
            try
            {
                emailVerification = await _context.EmailVerifications.Where(em => em.Email.Equals(email)).FirstOrDefaultAsync();
            }
            catch
            {
                return false;
            }
            if (emailVerification==null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckVerification(string email, string verifiCode)
        {
            DateTime dateTime = DateTime.UtcNow;
            EmailVerification? emailVerification = await _context.EmailVerifications.
                Where((em) => em.Email.Equals(email) && em.VerificationCode.Equals(verifiCode)
                && em.ExpirationTime >= dateTime).
                FirstOrDefaultAsync();
            if (emailVerification is null)
            {
                return false;
            }
            return true;
        }
    }
}
