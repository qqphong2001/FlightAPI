using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetAPI.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public int ticketclassId { get; set; }

    

        public string PaymentMethods { get; set; }

        public int FlightID { get; set; }

        public int VoucherID { get; set; }

        public string CodeSeats { get; set; }

        public int tempId { get; set; }


    }
}
