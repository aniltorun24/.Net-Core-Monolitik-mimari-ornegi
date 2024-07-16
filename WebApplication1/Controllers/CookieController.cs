using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class CookieController : Controller
    {

        
        public IActionResult CookieCreate()
        {

            HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(2)//oluşturulan cookienin ömrü 2 gün olsun.
            });
            return View();
        }
        public IActionResult CookieRead() {
            var bgcolor = HttpContext.Request.Cookies["background-color"];//cookie değerini okumak için 
            ViewBag.bgcolor = bgcolor;
            return View();
            
        }
    }
}
