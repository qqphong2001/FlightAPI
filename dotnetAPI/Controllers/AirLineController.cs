using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirLineController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AirLineController(ApplicationDbContext db)
        {

            _db = db;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var AirLines = _db.AirLines.ToList();

            return Ok(AirLines);

        }

        [HttpPost("Create")]
        //public IActionResult Create(AirLine obj)
        //{

        //    var AirLines = _db.AirLines.Add(obj);
        //    _db.SaveChanges();


        //    return Ok(
        //        new
        //        {
        //            Success = true,
        //            Message = "Tạo mới thành công"
        //        }

        //        );

        //}
        public async Task<IActionResult> CreateAirline([FromBody] AirLine airline)
        {
            _db.AirLines.Add(airline);
            await _db.SaveChangesAsync();

            return Ok(
                 new
               {
                    Success = true,
                   Message = "Tạo mới thành công"
               }
              );
        }
    

    [HttpPut("Update")]

        public IActionResult Update(AirLine obj)
        {

            var AirLines = _db.AirLines.Find(obj);

            AirLines.Name = obj.Name;
            AirLines.Logo = obj.Logo;

            _db.AirLines.Update(AirLines);
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
            var AirLines = _db.AirLines.Find(id);

            _db.AirLines.Remove(AirLines);
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
            var AirLines = _db.AirLines.Find(id);

            return Ok(AirLines);

        }

    }
}
