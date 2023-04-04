using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class FlightRouteDetail
    {
        [Key]
        public int Id { get; set; } 
        public int FlightRouteId { get; set; }
        public int BeginAirPortId { get; set; }
        public int EndAirPortId { get; set; }

    }
}
