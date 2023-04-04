using System;

namespace dotnetAPI.Model
{
    public class BookingTicket
    {
       public int Id { get; set; }

        public DateTime bookingDate { get; set; }


        public int customerId { get; set; }
        public int ticketId { get; set; }


        public float ticketprice { get; set; }
    }
}
