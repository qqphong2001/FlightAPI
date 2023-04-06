using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using dotnetAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public const string CARTKEY = "tickets";
        private readonly IHttpContextAccessor _context;

        private readonly HttpContext HttpContext;
        private readonly session _session;
        public InvoiceController(ApplicationDbContext db, IHttpContextAccessor context, session session)
        {
            _context = context;
            HttpContext = context.HttpContext;
            _db = db;
            _session = session;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var invoice = _db.Invoice.Join(
                _db.Ticket,
                invoice => invoice.idTicket,
                ticket => ticket.Id,
                (invoice , ticket) => new { invoice = invoice , ticket = ticket })
                .Join(
                _db.TempCustomer,
                invoiceCus => invoiceCus.ticket.tempId,
                tempCus => tempCus.Id, 
                (invoiceCus, tempCus) => new { invoice = invoiceCus.invoice , ticket = invoiceCus.ticket , tempcustomer = tempCus }).
                Join(
                _db.nationCCID,
                invoiceNation => invoiceNation.tempcustomer.nationCCIDID,
                nation => nation.Id,
                (invoiceNation, nation) => new { invoice = invoiceNation.invoice, ticket = invoiceNation.ticket, tempcustomer = invoiceNation.tempcustomer ,nation = nation }).ToList();
                
                ; 

            return Ok(invoice);

        }
        [HttpPost("Create")]
        public IActionResult Create()
        {
            var session = _session.GetCartItems();



            var all = session.Join(
                 _db.TempCustomer,
                 ticket => ticket.ticket.tempId,
                 customer => customer.Id,
                 (ticket, customer) => new { ticket = ticket, customer = customer })
                 .Join(
                 _db.nationCCID,
                 ticketCCID => ticketCCID.customer.nationCCIDID,
                 CCID => CCID.Id,
                 (ticketCCID, CCID) => new { ticket = ticketCCID.ticket, customer = ticketCCID.customer, CCID = CCID }).
                 Join(
                 _db.Flights,
                 ticketFlight => ticketFlight.ticket.ticket.FlightID,
                 flight => flight.Id,
                 (ticketFlight, flight) => new { ticket = ticketFlight.ticket, customer = ticketFlight.customer, CCID = ticketFlight.CCID, flight = flight }).
                 Join(
                 _db.Prices,
                 ticketPrice => ticketPrice.flight.Id,
                 price => price.FlightID,
                 (ticketPrice, price) => new { ticket = ticketPrice.ticket, customer = ticketPrice.customer, CCID = ticketPrice.CCID, flight = ticketPrice.flight, price = price }
                 ).ToList();
            float total = 0;

            foreach (var item in all)
            {
                total += item.price.price;
            }


            var One = session.Join(
               _db.TempCustomer,
               ticket => ticket.ticket.tempId,
               customer => customer.Id,
               (ticket, customer) => new { ticket = ticket, customer = customer })
               .Join(
               _db.nationCCID,
               ticketCCID => ticketCCID.customer.nationCCIDID,
               CCID => CCID.Id,
               (ticketCCID, CCID) => new { ticket = ticketCCID.ticket, customer = ticketCCID.customer, CCID = CCID }).
               Join(
               _db.Flights,
               ticketFlight => ticketFlight.ticket.ticket.FlightID,
               flight => flight.Id,
               (ticketFlight, flight) => new { ticket = ticketFlight.ticket, customer = ticketFlight.customer, CCID = ticketFlight.CCID, flight = flight }).
               Join(
               _db.Prices,
               ticketPrice => ticketPrice.flight.Id,
               price => price.FlightID,
               (ticketPrice, price) => new { ticket = ticketPrice.ticket, customer = ticketPrice.customer, CCID = ticketPrice.CCID, flight = ticketPrice.flight, price = price }
               ).FirstOrDefault();



                var Invoices = new Invoice
            {
              
                PaymentDate = DateTime.Now,
                Amount = total,
                PaymentStatus = "Đã thanh toán",
                CustomerName = One.CCID.lastName,
                idTicket = One.ticket.ticket.Id

            };

            _db.Invoice.Add(Invoices);
            _db.SaveChanges();

            _session.ClearCart();
            return Ok(new
            {
                Success = true,
            }

                );

        }
    }
}
