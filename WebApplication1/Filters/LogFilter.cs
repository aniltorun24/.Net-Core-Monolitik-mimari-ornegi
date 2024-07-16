using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplication1.Filters
{
    public class LogFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action method çalıştırılmadan önce");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action method çalıştırıldıktan sonra");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Action method sonuç üretmeden önce");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("Action method sonuç ürettikten sonra");
        }
    }
}
