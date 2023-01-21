using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Subscriber.Services.Models;

public class CardModel
{
    public int Id { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public float BMI { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public Data.Entities.Subscriber Subscriber { get; set; }

}
