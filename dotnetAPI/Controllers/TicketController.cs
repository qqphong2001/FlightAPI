using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using dotnetAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {

            var tickets = _db.Ticket.Join(
                _db.TempCustomer,
                ticketCus => ticketCus.tempId,
                customer => customer.Id,
                (ticketCus, customer) => new { ticket = ticketCus, customer = customer }
                ).Join(
                _db.nationCCID,
                ticketCCID => ticketCCID.customer.nationCCIDID,
                CCID => CCID.Id,
                (ticketCCID, CCID) => new { ticket = ticketCCID.ticket, customer = ticketCCID.customer, CCID = CCID }
                ).Join(
                _db.Flights,
                ticketflight => ticketflight.ticket.FlightID,
                flight => flight.Id,
                (ticketflight, flight) => new { ticket = ticketflight.ticket, customer = ticketflight.customer, CCID = ticketflight.CCID , flight = flight }  
                ).Join(
                _db.Prices,
                ticketprice => ticketprice.flight.Id,
                price => price.FlightID,
                (ticketprice, price) => new { ticket = ticketprice.ticket, customer = ticketprice.customer, CCID = ticketprice.CCID, flight = ticketprice.flight , price = price }
                ).Join(
                _db.AirLines,
                ticketAir => ticketAir.flight.AirLineId,
                airline => airline.Id,
                (ticketAir, airline) => new { ticket = ticketAir.ticket, customer = ticketAir.customer, CCID = ticketAir.CCID, flight = ticketAir.flight, price = ticketAir.price , airline = airline })
                .Join(
                    _db.FlightsRoute,
                    ticketroute => ticketroute.flight.FlightRouteID,
                    route => route.Id,
                    (ticketroute, route)   => new { ticket = ticketroute.ticket, customer = ticketroute.customer, CCID = ticketroute.CCID, flight = ticketroute.flight, price = ticketroute.price, airline = ticketroute.airline ,route =route })
                .Join(
                _db.FlightRouteDetail,
                flightdetail => flightdetail.route.Id,
                detail => detail.FlightRouteId,
                (flightdetail, detail) => new { ticket = flightdetail.ticket, customer = flightdetail.customer, CCID = flightdetail.CCID, flight = flightdetail.flight, price = flightdetail.price, airline = flightdetail.airline, route = flightdetail.route , routeDetail = detail }
                ).
                Join(
                _db.airPorts,
                flightBegin => flightBegin.routeDetail.BeginAirPortId,
                begin => begin.Id,
                (flightBegin, begin) => new { ticket = flightBegin.ticket, customer = flightBegin.customer, CCID = flightBegin.CCID, flight = flightBegin.flight, price = flightBegin.price, airline = flightBegin.airline, route = flightBegin.route, routeDetail = flightBegin.routeDetail , begin = begin }
                ).Join(
                  _db.airPorts,
                flightEnd => flightEnd.routeDetail.EndAirPortId,
                end => end.Id,
                (flightEnd, end) => new { ticket = flightEnd.ticket, customer = flightEnd.customer, CCID = flightEnd.CCID, flight = flightEnd.flight, price = flightEnd.price, airline = flightEnd.airline, route = flightEnd.route, routeDetail = flightEnd.routeDetail, begin = flightEnd.begin ,end =end }
                ).ToList();
                ;

            return Ok(tickets);

        }

        [HttpGet("Get")]

        public IActionResult Get(int id)
        {
            var tickets = _db.Ticket.Find(id);

            return Ok(tickets);
        }


    }
}
