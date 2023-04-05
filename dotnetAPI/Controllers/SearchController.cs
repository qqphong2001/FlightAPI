//using dotnetAPI.AppDbContext;
//using dotnetAPI.Model;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Routing;
//using System;
//using System.Linq;
//using System.Net.Sockets;

//namespace dotnetAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {
//        private readonly ApplicationDbContext _db;
//        public SearchController(ApplicationDbContext db)
//        {

//            _db = db;
//        }

//        [HttpGet("Get/Route")]
//        public IActionResult Get(int idBegin, int idEnd, DateTime timeFly)
//        {
//            var timeFlys = timeFly.Date;
//            var flight = _db.Flights.Join(_db.Statuss,
//              flights => flights.StatusId,
//              status => status.Id,
//              (flights, status) => new { flights = flights, status = status }
//              ).Join(
//              _db.FlightsRoute,
//              flightRoute => flightRoute.flights.FlightRouteID,
//              route => route.Id,
//              (flightRoute, route) => new { flights = flightRoute.flights, status = flightRoute.status, route = route }
//              ).Join(
//              _db.Prices,
//              flightprice => flightprice.flights.Id,
//              price => price.FlightID,
//              (flightprice, price) => new { flights = flightprice.flights, status = flightprice.status, route = flightprice.route, price = price }
//              ).
//              Join(
//              _db.AirLines,
//              flightLine => flightLine.flights.AirLineId,
//              airLine => airLine.Id,
//              (flightLine, airLine) => new { flights = flightLine.flights, status = flightLine.status, route = flightLine.route, price = flightLine.price, airLine = airLine }
//              ).
//              Join(
//              _db.FlightRouteDetail,
//              flightdetail => flightdetail.route.Id,
//              detail => detail.FlightRouteId,
//              (flightdetail, detail) => new { flights = flightdetail.flights, status = flightdetail.status, route = flightdetail.route, price = flightdetail.price, airLine = flightdetail.airLine, detail = detail }
//              ).
//              Join(
//              _db.airPorts,
//              flightBegin => flightBegin.detail.BeginAirPortId,
//              begin => begin.Id,
//              (flightBegin, begin) => new { flights = flightBegin.flights, status = flightBegin.status, route = flightBegin.route, price = flightBegin.price, airLine = flightBegin.airLine, detail = flightBegin.detail, begin = begin }
//              ).Join(
//                _db.airPorts,
//              flightEnd => flightEnd.detail.EndAirPortId,
//              end => end.Id,
//              (flightEnd, end) => new { flights = flightEnd.flights, status = flightEnd.status, route = flightEnd.route, price = flightEnd.price, airLine = flightEnd.airLine, detail = flightEnd.detail, begin = flightEnd.begin, end = end }
//              ).Where(x => x.detail.BeginAirPortId == idBegin && x.detail.EndAirPortId == idEnd && x.flights.ArrivalTime.Date == timeFlys).
//              ToList();


//            return Ok(flight);
//        }

//        [HttpGet("Get/Invoice")]
//        public IActionResult GetInvoice(int InvoiceID)
//        {


//            var flight = _db.Flights.Join(_db.Statuss,
//            flights => flights.StatusId,
//            status => status.Id,
//            (flights, status) => new { flights = flights, status = status }
//            ).Join(
//            _db.FlightsRoute,
//            flightRoute => flightRoute.flights.FlightRouteID,
//            route => route.Id,
//            (flightRoute, route) => new { flights = flightRoute.flights, status = flightRoute.status, route = route }
//            ).Join(
//            _db.Prices,
//            flightprice => flightprice.flights.Id,
//            price => price.FlightID,
//            (flightprice, price) => new { flights = flightprice.flights, status = flightprice.status, route = flightprice.route, price = price }
//            ).
//            Join(
//            _db.AirLines,
//            flightLine => flightLine.flights.AirLineId,
//            airLine => airLine.Id,
//            (flightLine, airLine) => new { flights = flightLine.flights, status = flightLine.status, route = flightLine.route, price = flightLine.price, airLine = airLine }
//            ).
//            Join(
//            _db.FlightRouteDetail,
//            flightdetail => flightdetail.route.Id,
//            detail => detail.FlightRouteId,
//            (flightdetail, detail) => new { flights = flightdetail.flights, status = flightdetail.status, route = flightdetail.route, price = flightdetail.price, airLine = flightdetail.airLine, detail = detail }
//            ).
//            Join(
//            _db.airPorts,
//            flightBegin => flightBegin.detail.BeginAirPortId,
//            begin => begin.Id,
//            (flightBegin, begin) => new { flights = flightBegin.flights, status = flightBegin.status, route = flightBegin.route, price = flightBegin.price, airLine = flightBegin.airLine, detail = flightBegin.detail, begin = begin }
//            ).Join(
//              _db.airPorts,
//            flightEnd => flightEnd.detail.EndAirPortId,
//            end => end.Id,
//            (flightEnd, end) => new { flights = flightEnd.flights, status = flightEnd.status, route = flightEnd.route, price = flightEnd.price, airLine = flightEnd.airLine, detail = flightEnd.detail, begin = flightEnd.begin, end = end }
//            ).Join(
//                _db.Ticket,
//                flightinvoice => flightinvoice.flights.Id,
//                ticket => ticket.Id,
//                (flightinvoice, ticket) => new { flights = flightinvoice.flights, status = flightinvoice.status, route = flightinvoice.route, price = flightinvoice.price, airLine = flightinvoice.airLine, detail = flightinvoice.detail, begin = flightinvoice.begin, end = flightinvoice.end, ticket = ticket }
//                ).Join(
//                _db.Invoice,
//                flightInvoice => flightInvoice.ticket.Id,
//                invoice => invoice.Id,
//                (flightInvoice, invoice) => new { flights = flightInvoice.flights, status = flightInvoice.status, route = flightInvoice.route, price = flightInvoice.price, airLine = flightInvoice.airLine, detail = flightInvoice.detail, begin = flightInvoice.begin, end = flightInvoice.end, ticket = flightInvoice.ticket, invoice = invoice }
//                ).Where(x => x.invoice.Id == InvoiceID).ToList();


//            return Ok(flight);
//        }


//    }
//}
