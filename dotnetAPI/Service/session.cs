using dotnetAPI.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace dotnetAPI.Service
{
    public class session
    {
        public const string CARTKEY = "Session";
        private readonly IHttpContextAccessor _context;

        private readonly HttpContext HttpContext;

        public session(IHttpContextAccessor context)
        {
            _context = context;
            HttpContext = context.HttpContext;
        }

        // Lấy cart từ Session (danh sách CartItem)
        public List<Session> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Session>>(jsoncart);
            }
            return new List<Session>();
        }

        // Xóa cart khỏi session
        public void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(List<Session> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
