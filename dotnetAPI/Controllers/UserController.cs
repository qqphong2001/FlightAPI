using dotnetAPI.AppDbContext;
using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSetting _Appsetting;
        public UserController(ApplicationDbContext db,IOptionsMonitor<AppSetting> optionsMonitor) 
        {
            _db = db;
            _Appsetting = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        //public IActionResult Validate  (Login obj)
        //{
        //    var user = _db.Users.FirstOrDefault(x => x.UserName == obj.UserName && x.Password == obj.Password);

        //    if(user == null)
        //    {
        //        return Ok(new
        //        {
        //            Success = false,
        //            Message = "Tài khoản hoặc mật khẩu không đúng"
        //        });
        //    }

        //    return Ok(new
        //    {
        //        Success = true,
        //        Message = "Đăng nhập thành công",
        //        Data = Token(user)
        //    });
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (existingUser == null)
            {
<<<<<<< Updated upstream
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Tài khoản hoặc mật khẩu không đúng"
                });
=======
                return Unauthorized();
>>>>>>> Stashed changes
            }

            return Ok(new
            {
                Success = true,
                Message = "Đăng nhập thành công",
            }

                );
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Tạo tài khoản thành công"
            });
        }

        private TokenModel Token(User obj)
        {
            var JwtToken = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_Appsetting.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("UserName", obj.UserName),
                new Claim("Id", obj.Id.ToString()),

                new Claim(ClaimTypes.Email, obj.Email),

                new Claim("TokenId",Guid.NewGuid().ToString()),


                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha256Signature )
             
            };

            var token = JwtToken.CreateToken(tokenDescription);

            var accessToken =  JwtToken.WriteToken(token);

            return new TokenModel { 
                AccessToken = accessToken,
                RefreshToken = CreateRefreshToken()
            };

        }


        private string CreateRefreshToken()
        {
           var random = new byte[32];
            using (var ram = RandomNumberGenerator.Create())
            {
                ram.GetBytes(random);
                return Convert.ToBase64String(random);
            }

        }
    }
}
