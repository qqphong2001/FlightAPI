using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRouteController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public FlightRouteController(ApplicationDbContext db) {
        
            _db = db;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var flight = _db.FlightsRoute.Join(
                _db.FlightRouteDetail,
                route => route.Id,
                detail => detail.FlightRouteId,
                (route, detail) => new { route = route, detail = detail }).Join(
                _db.airPorts,
                routebegin => routebegin.detail.BeginAirPortId,
            begin => begin.Id,
                (routebegin , begin) => new { route = routebegin.route, detail = routebegin.detail,begin = begin }).
                Join(
                _db.airPorts,
                routeend => routeend.detail.EndAirPortId,
                end => end.Id,
                (routeend , end) => new { route = routeend.route, detail = routeend.detail, begin = routeend.begin,end =end }).
                
                ToList();

            return Ok(flight);

        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] FlightRouteInputModel inputModel)
        {
            var flightRoute = _db.FlightsRoute.Add(inputModel.FlightRoute);
            _db.SaveChanges();

            var flightRouteDetail = new FlightRouteDetail
            {
                FlightRouteId = flightRoute.Entity.Id,
                BeginAirPortId = inputModel.FlightRouteDetail.BeginAirPortId,
                EndAirPortId = inputModel.FlightRouteDetail.EndAirPortId
            };
            _db.FlightRouteDetail.Add(flightRouteDetail);
            _db.SaveChanges();
            return Ok(new
            {
                Success = true,
                Message = "Tạo mới chuyến bay thành công"
            });
        }

        [HttpPut("Update")]

        public IActionResult Update(dotnetAPI.Model.FlightRoute obj)
        {

            var flightRoute = _db.FlightsRoute.Find(obj);

            flightRoute.Distance = obj.Distance;
    
        
            flightRoute.FlightTime = obj.FlightTime;
            
            _db.FlightsRoute.Update(flightRoute);
            _db.SaveChanges();


            return Ok(
                new
                {
                    Success = true,
                    Message = "Chỉnh sửa thành công"
                });
        }

        [HttpDelete("detele/{id}")]
        public IActionResult delete(int id)
        {
            var route = _db.FlightsRoute.Find(id);

            _db.FlightsRoute.Remove(route);
            _db.SaveChanges();

            return Ok(
                new
                {
                    Success = true,
                    Message = "Xóa thành công"
                }
                );
        }

        [HttpGet("Get")]

        public IActionResult Get(int id)
        {
            var flight = _db.FlightsRoute.Join(
               _db.FlightRouteDetail,
               route => route.Id,
               detail => detail.FlightRouteId,
               (route, detail) => new { route = route, detail = detail }).Join(
               _db.airPorts,
               routebegin => routebegin.detail.BeginAirPortId,
           begin => begin.Id,
               (routebegin, begin) => new { route = routebegin.route, detail = routebegin.detail, begin = begin }).
               Join(
               _db.airPorts,
               routeend => routeend.detail.EndAirPortId,
               end => end.Id,
               (routeend, end) => new { route = routeend.route, detail = routeend.detail, begin = routeend.begin, end = end }).
        
               ToList();

            return Ok(flight);

        }



    }
}
