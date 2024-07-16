using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CodeActions;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly AppDbContext _context;
        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First(); //action metodların aldığı ilk parametreyi getirir.
            var id = (int)idValue;
            var hasProduct = _context.Products.Any(x => x.Id == id);
            if (hasProduct==false)
            {
                var result = new RedirectToActionResult("Error", "Home",  new ErrorViewModel()
                {
                    Errors = new List<string>() {$"Id({id})'ye sahip ürün veritabanında  bulunamamıştır." }
                });
            }
        }
    }
}
