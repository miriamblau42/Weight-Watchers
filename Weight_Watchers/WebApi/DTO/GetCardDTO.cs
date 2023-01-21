using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Subscriber.WebApi.DTO;

public class GetCardDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public float BMI { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
  
}
