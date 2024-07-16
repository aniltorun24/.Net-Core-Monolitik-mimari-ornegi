using AutoMapper;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<Product,ProductUpdateViewModel>().ReverseMap();
            CreateMap<Visitor,VisitorViewModel>().ReverseMap();
        }
    }
}
