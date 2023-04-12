using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class FlightRoute
    {
        [Key]
        public int Id { get; set; } 
        public string Distance { get; set; }
        public string FlightTime { get; set; }



    }
}
