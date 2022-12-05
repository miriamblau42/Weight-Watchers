using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Subscriber.Data.Entities
{
    public class Card
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public float BMI { get; set; }
        [Required]
        public float Height { get; set; }
        public float Weight { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OpenDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        public Guid SubscriberID { get; set; }

        [ForeignKey("SubscriberID")]
        [JsonIgnore]
        public virtual Subscriber Subscriber { get; set; }


    }
}
