using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        //blog/article/makale-name/id
        public IActionResult Article(string name,int id)
        {
            //var routes = Request.RouteValues["article"];

            return View();
        }
    }
}
