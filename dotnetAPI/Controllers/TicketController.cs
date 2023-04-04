using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using dotnetAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public const string CARTKEY = "tickets";
        private readonly IHttpContextAccessor _context;

        private readonly HttpContext HttpContext;
        private readonly session _Session;


        public TicketController(ApplicationDbContext db, IHttpContextAccessor context , session session)
        {
            _context = context;
            HttpContext = context.HttpContext;
            _db = db;
            _Session = session;
        }

        [HttpPost("Create")]
        public IActionResult Create(TicketCustomerInput obj)
        {




            _db.TempCustomer.Add(obj.customer);
            _db.SaveChanges();


            obj.Ticket.tempId = obj.customer.Id;

            var ticket = _db.Ticket.Add(obj.Ticket);
            _db.SaveChanges();

            var session = _Session.GetCartItems();
            session.Add(new Session() { quantity = 1, ticket = obj.Ticket });

            _Session.SaveCartSession(session);


            

            return Ok(obj.Ticket);
        }





    }
}
