using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Interfaces
{
    public interface IEmailVerificationDal
    {
        Task AddEmailVerification(EmailVerification emailVerification);
        Task<bool> CheckEmailVerifiedByEmail(string email);
        Task UpdateEmailVerification(EmailVerification emailVerification);
        Task<bool> CheckVerification(string email, string verifiCode);
    }
}
