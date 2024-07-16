using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class CustomExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;//Hatayı kendi ilemiz ile vereceğimizi söylüyoruz.
            var error = context.Exception.Message;

            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel()
            {
                Errors = new List<string>() { $"{error}" }
            });
        }
    }
}
