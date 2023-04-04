using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public float price { get; set; }

        public int FlightID { get; set; }

        public DateTime Date { get; set; }

    }
}
