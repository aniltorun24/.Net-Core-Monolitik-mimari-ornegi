using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorViewComponent(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var visitors = _context.Visitors.ToList();
            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);
            ViewBag.visitorViewModels = visitorViewModels;
            return View();
        }
        
    }
}
