using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketClassController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TicketClassController(ApplicationDbContext db)
        {

            _db = db;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var TicketClass = _db.TicketClass.ToList();

            return Ok(TicketClass);

        }

        [HttpPost("Create")]
        public IActionResult Create(TicketClass obj)
        {

           _db.TicketClass.Add(obj);
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

        public IActionResult Update(TicketClass obj)
        {

            var TicketClass = _db.TicketClass.Find(obj);

            TicketClass.Name = obj.Name;

            _db.TicketClass.Update(TicketClass);
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
            var TicketClass = _db.TicketClass.Find(id);

            _db.TicketClass.Remove(TicketClass);
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
            var TicketClass = _db.TicketClass.Find(id);

            return Ok(TicketClass);

        }
    }
}
