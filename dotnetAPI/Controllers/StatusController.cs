using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StatusController(ApplicationDbContext db)
        {

            _db = db;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var status = _db.Statuss.ToList();

            return Ok(status);

        }

        [HttpPost("Create")]
        public IActionResult Create(Status obj)
        {

            var flightRoute = _db.Statuss.Add(obj);
            _db.SaveChanges();


            return Ok(
                new
                {
                    Success = true,
                    Message = "Tạo mới trạng thái thành công"
                }

                );

        }

        [HttpPut("Update")]

        public IActionResult Update(Status obj)
        {

            var status = _db.Statuss.Find(obj);

            status.Name = obj.Name;

            _db.Statuss.Update(status);
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
            var status = _db.Statuss.Find(id);

            _db.Statuss.Remove(status);
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
            var status = _db.Statuss.Find(id);

            return Ok(status);

        }

    }
}
