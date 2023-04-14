using dotnetAPI.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CCID : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public CCID(ApplicationDbContext db)
        {
            
            _db = db;
        }

        [HttpGet("Get")]
        public IActionResult Get(int id) { 
        
           var ccid =  _db.nationCCID.Find(id);
            

            return Ok(ccid);


        }
    }
}
