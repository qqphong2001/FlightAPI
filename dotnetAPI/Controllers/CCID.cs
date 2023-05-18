using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
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
        public nationCCID Get(int id)
        {

            var ccid =  _db.nationCCID.Find(id);


            //return Ok(ccid);
            return ccid;


        }
    }
}
