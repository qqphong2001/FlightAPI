using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public FlightController( ApplicationDbContext db)
        {
              _db = db;
        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {

            var flight = _db.Flights.Join(
                _db.Statuss,
                flights => flights.StatusId,
                status => status.Id,
                (flights, status) => new { flights = flights , status = status }
                ).Join(
                _db.FlightsRoute,
                flightRoute => flightRoute.flights.FlightRouteID,
                route => route.Id,
                (flightRoute, route) => new { flights = flightRoute.flights , status = flightRoute.status , route = route }
                )
                .
                Join(
                _db.Prices,
                flightprice => flightprice.flights.Id,
                price => price.FlightID,
                (flightprice, price) => new { flights = flightprice.flights, status = flightprice.status, route = flightprice.route, price = price }
                )
                .
                Join(
                _db.AirLines,
                flightLine => flightLine.flights.AirLineId,
                airLine => airLine.Id,
                (flightLine, airLine) => new { flights = flightLine.flights, status = flightLine.status, route = flightLine.route, price = flightLine.price, airLine = airLine }
                ).
                Join(
                _db.FlightRouteDetail,
                flightdetail => flightdetail.route.Id,
                detail => detail.FlightRouteId,
                (flightdetail, detail) => new { flights = flightdetail.flights, status = flightdetail.status, route = flightdetail.route, price = flightdetail.price, airLine = flightdetail.airLine, detail = detail }
                ).
                Join(
                _db.airPorts,
                flightBegin => flightBegin.detail.BeginAirPortId,
                begin => begin.Id,
                (flightBegin, begin) => new { flights = flightBegin.flights, status = flightBegin.status, route = flightBegin.route, price = flightBegin.price, airLine = flightBegin.airLine, detail = flightBegin.detail, begin = begin }
                )
                .Join(
                  _db.airPorts,
                flightEnd => flightEnd.detail.EndAirPortId,
                end => end.Id,
                (flightEnd, end) => new { flights = flightEnd.flights, status = flightEnd.status, route = flightEnd.route, price = flightEnd.price, airLine = flightEnd.airLine, detail = flightEnd.detail, begin = flightEnd.begin, end = end }
                )
                .
                ToList();
                
           
            return Ok(flight);
        }

        [HttpGet("Get/{id}")]

        public IActionResult Get(int id)
        {
           
            var flight = _db.Flights.Join(_db.Statuss,
               flights => flights.StatusId,
               status => status.Id,
               (flights, status) => new { flights = flights, status = status }
               ).Join(
               _db.FlightsRoute,
               flightRoute => flightRoute.flights.FlightRouteID,
               route => route.Id,
               (flightRoute, route) => new { flights = flightRoute.flights, status = flightRoute.status, route = route }
               ).Join(
               _db.Prices,
               flightprice => flightprice.flights.Id,
               price => price.FlightID,
               (flightprice, price) => new { flights = flightprice.flights, status = flightprice.status, route = flightprice.route, price = price }
               ).
               Join(
               _db.AirLines,
               flightLine => flightLine.flights.AirLineId,
               airLine => airLine.Id,
               (flightLine, airLine) => new { flights = flightLine.flights, status = flightLine.status, route = flightLine.route, price = flightLine.price, airLine = airLine }
               ).
               Join(
               _db.FlightRouteDetail,
               flightdetail => flightdetail.route.Id,
               detail => detail.FlightRouteId,
               (flightdetail, detail) => new { flights = flightdetail.flights, status = flightdetail.status, route = flightdetail.route, price = flightdetail.price, airLine = flightdetail.airLine, detail = detail }
               ).
               Join(
               _db.airPorts,
               flightBegin => flightBegin.detail.BeginAirPortId,
               begin => begin.Id,
               (flightBegin, begin) => new { flights = flightBegin.flights, status = flightBegin.status, route = flightBegin.route, price = flightBegin.price, airLine = flightBegin.airLine, detail = flightBegin.detail, begin = begin }
               ).Join(
                 _db.airPorts,
               flightEnd => flightEnd.detail.EndAirPortId,
               end => end.Id,
               (flightEnd, end) => new { flights = flightEnd.flights, status = flightEnd.status, route = flightEnd.route, price = flightEnd.price, airLine = flightEnd.airLine, detail = flightEnd.detail, begin = flightEnd.begin, end = end }
               ).Where(x => x.flights.Id == id).FirstOrDefault();
               
            return Ok(flight);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var flight = _db.Flights.Find(id);

            _db.Flights.Remove(flight);
            _db.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Xóa thành công"
            });
        }

        [HttpPost("Create")]
        public IActionResult Create(FlightPriceInputModel obj)
        {
            var Flights = new Flight
            {
                Seats = obj.flight.Seats,
                ArrivalTime = obj.flight.ArrivalTime,
                StatusId = obj.flight.StatusId,
                FlightRouteID = obj.flight.FlightRouteID,
                AirLineId = obj.flight.AirLineId,
                DepartureTime = obj.flight.DepartureTime,
                timeFly = obj.flight.timeFly,
                CodeFlight = obj.flight.CodeFlight
            };

            var flight = _db.Flights.Add(Flights);
            _db.SaveChanges();

            var Price = new Price
            {
                Date = DateTime.Now,
                price = obj.price.price,
                FlightID = flight.Entity.Id,

            };
            _db.Prices.Add(Price);
            _db.SaveChanges();


            return Ok( new
            {
                Success = true,
                Message = "Tạo chuyến bay thành công"
            });

        }

        [HttpPut("Update/{id}")]

        public IActionResult Update(Flight obj)
        {

            var flight = _db.Flights.Find(obj.Id);

           
            flight.ArrivalTime = obj.ArrivalTime;
            flight.DepartureTime = obj.DepartureTime;
            flight.StatusId = obj.StatusId;
            flight.Seats    = obj.Seats;
            flight.AirLineId = obj.AirLineId;
            flight.FlightRouteID = obj.FlightRouteID;
            flight.CodeFlight = obj.CodeFlight;
            flight.timeFly = obj.timeFly;



            _db.Flights.Update(flight);
            _db.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Cập nhật chuyến bay thành công"
            });



        }


    }
}
