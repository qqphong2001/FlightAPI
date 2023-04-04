using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public AirPortController(ApplicationDbContext db)
        {

            _db = db;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var AirPort = _db.airPorts.ToList();

            return Ok(AirPort);

        }

        [HttpPost("Create")]
        public IActionResult Create(AirPort obj)
        {

            var AirPort = _db.airPorts.Add(obj);
            _db.SaveChanges();


            return Ok(
                new
                {
                    Success = true,
                    Message = "Tạo mới thành công"
                }

                );

        }

        [HttpPut("Update")]

        public IActionResult Update(AirPort obj)
        {

            var AirPort = _db.airPorts.Find(obj.Id);

            AirPort.Name = obj.Name;
            AirPort.Location = obj.Location;
            AirPort.IATA = obj.IATA;

            _db.airPorts.Update(AirPort);
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
            var AirPort = _db.airPorts.Find(id);

            _db.airPorts.Remove(AirPort);
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
            var AirPort = _db.airPorts.Find(id);

            return Ok(AirPort);

        }


    }
}
