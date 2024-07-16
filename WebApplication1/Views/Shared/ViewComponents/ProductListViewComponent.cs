using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Views.Shared.ViewComponents
{
   //[ViewComponent(Name ="p-list")]//view componenta istediğimiz ismi vermemizi sağlıyor
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int type=1)
        {
            var viewmodels = _context.Products.Select(x=> new ProductListComponentViewModel()
            {
                Name =x.Name,
                Description=x.Desciription
            }).ToList();
            if(type == 1)
            {
                return View("Default",viewmodels);
            }
            else {
                return View("Type2", viewmodels);
            }


            

        }
    }
}
