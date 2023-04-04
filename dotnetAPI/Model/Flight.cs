using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class Flight 
    {
        [Key]
        public int Id { get; set; } 

        public int Seats { get; set; }

        public DateTime ArrivalTime { get; set; }
      
        public int StatusId { get; set; }

        public int FlightRouteID { get; set; }
      public int AirLineId { get; set; }
        public DateTime DepartureTime { get; set;}

        public DateTime timeFly { get; set; }
        public string CodeFlight { get; set; }
    }
}
