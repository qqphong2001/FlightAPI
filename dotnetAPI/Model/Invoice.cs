using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class Invoice
    {
        [Key]         
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }

        public float Amount { get; set; }

        public string PaymentStatus { get; set; }

        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public int RevenueID { get; set; }

        public int IDAdmin { get; set; }

        public int idTicket { get; set; }

    }
}
