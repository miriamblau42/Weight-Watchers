using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services.Models
{
    public class CardModel
    {
        public int ID { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid subscriberID { get; set; }
    }
}
