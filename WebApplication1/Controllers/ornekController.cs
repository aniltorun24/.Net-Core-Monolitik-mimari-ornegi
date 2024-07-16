using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [CustomResultFilter("x-version","1.0")]
    public class ornekController : Controller
    {
        public IActionResult Index()
        {
            var productList = new List<Product2>()
            {
                new() { Id = 1, Name = "Kalem" },
                new() { Id = 2, Name = "Defter" },
                new() { Id = 3, Name = "Silgi" }
            };
            ViewBag.name = ".net asp";
            
            ViewData["age"] = 30;
            ViewData["names"] = new List<string>() { "ahmet", "mehmet", "hasan" };
            return View(productList);
        }
        public IActionResult Parametreview(int id)
        {
            return RedirectToAction("JsonResultParametre", "ornekController1", new { id = id });
        }
        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id=id });
        }
        public IActionResult ContentResult()
        {
            return Content("ContentResult");
        }
        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "kalem 1", price = 100 });
        }
    }
}
