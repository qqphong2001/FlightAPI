using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PriceController(ApplicationDbContext db) {

                
            _db = db;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
           

            return Ok(_db.Prices.ToList());
        }

        [HttpPost("Create")]

        public IActionResult Create(Price obj)
        {

            _db.Prices.Add(obj);
            _db.SaveChanges();
            return Ok(new
            {
                Success = true,
                Message = "Tạo thành công"
            });
        }

        [HttpGet("Get/{id}")]

        public IActionResult Get(int id)
        {
            var price  = _db.Prices.Find(id);

            return Ok(price);
        }
        [HttpDelete("Delete")]

        public IActionResult Delete(int id)
        {
            var price = _db.Prices.Find(id);
            if (price == null)
            {
                return BadRequest();
            }

            _db.Prices.Remove(price);
            _db.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Xóa thành công"
            });

        }

        [HttpPut("Update")]

        public IActionResult Update(Price price) {
            var prices = _db.Prices.Find(price.Id);

            prices.price = price.price;
            prices.Date = price.Date;
            prices.FlightID = price.FlightID;
            _db.Prices.Update(prices);
            _db.SaveChanges();

            return Ok(
                new
                {
                    Success = true,
                    Message = "Chỉnh sửa thành công"
                }
                );

            
        }



    }
}
