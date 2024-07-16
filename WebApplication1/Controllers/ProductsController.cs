using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Net.Http.Headers;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.ViewModels;
namespace WebApplication1.Controllers;

[Route("[controller]/[action]")]
public class ProductsController : Controller  
{
    private AppDbContext _context;

    private readonly IFileProvider _fileProvider;

    private readonly IMapper _mapper;
    private readonly ProductRepository _productRepository;


    public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
    {
        _mapper = mapper;
        _productRepository = new ProductRepository();
        _context = context;
        //if (!_context.Products.Any())
        //{
        //    _context.Products.Add(new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "red",  }) ; 
        //    _context.Products.Add(new Product() { Name = "Kalem2", Price = 100, Stock = 200, Color = "red",   });
        //    _context.Products.Add(new Product() { Name = "Kalem3", Price = 100, Stock = 300, Color = "red",  });
        //    _context.SaveChanges();
        //}


        if (!_productRepository.GetAll().Any())
        {

        }
        _fileProvider = fileProvider;
    }
    //[CacheResourseFilter]
    public IActionResult Index()
    {
        List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x =>
        new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Color = x.Color,
            Stock = x.Stock,
            CategoryName = x.Category.Name,
            Desciription = x.Desciription,
            Expire = x.Expire,
            ImagePath = x.ImagePath,
            IsPublish = x.IsPublish,
            PublichDate = x.PublichDate,


        }).ToList();


        return View(products);
    }
    /*[ServiceFilter(typeof(NotFoundFilter))]*/ //başına servicefilter yazdık çünkü bu filterın constractorında parametre geçiyor.
    [Route("urunler/urun/{productid}",Name ="product")]//bu şekilde de kullanılabilir.ya da action yazan yere herhangi bir türkçe karekter mesela urun yazabilirim.
    public IActionResult GetById(int productid)
    {
        var product=_context.Products.Find(productid);
        return View(_mapper.Map<ProductViewModel>(product));
    }
    [Route("[controller]/[action]/{page}/{pageSize}",Name ="productpage")]
    public IActionResult Pages(int page,int pageSize)
    {
        var products=_context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();//page 1 ve page size 3 iken skip ile o ürünü alıyoruz take metodu ile de ilk kaç tanesini alacağımızın kararını vermiş oluyoruz.
        ViewBag.page=page;
        ViewBag.pageSize = pageSize;
        return View(_mapper.Map<List<ProductViewModel>>(products));
    }
    //[ServiceFilter(typeof(NotFoundFilter))]
    [HttpGet("{id}")]
    public IActionResult Remove(int id)
    {
        var product = _context.Products.Find(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        //_productRepository.remove(id);
        return RedirectToAction("Index");
        //return View(product);
    }
    [HttpGet]//KULLANICININ GÖRDÜĞÜ ARAYÜZDEKİ İŞLEMLERi yapan metodlardır
    public ActionResult Add()
    {
        ViewBag.Expire = new Dictionary<string, int>()
        {
            {"1 Ay",1 },
            {"3 Ay",3 },
            {"6 Ay",6 },
            {"12 Ay",12 },

        };

        ViewBag.SelectColor = new SelectList(new List<ColorSelectList>(){
        new(){Data="Mavi",Value="Mavi"},
        new(){Data="Kırmızı",Value="Kırmızı"},
        new(){Data="Sarı",Value="Sarı"},
        }, "Value", "Data");

        var categories = _context.Category.ToList();
        ViewBag.categorySelect = new SelectList(categories, "Id", "Name");



        return View();
    }
    [HttpPost]//kullanıcı butona bastıktan sonra arka tarafta çalışan kodlardır.bir viewı yoktur.
    public IActionResult Add(ProductViewModel newproduct)
    {
        IActionResult result = null;
        //1.yöntem
        //var name = HttpContext.Request.Form["Name"].ToString();
        //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
        //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
        //var color = HttpContext.Request.Form["Color"].ToString();

        //Product newproduct = new Product() { Name = name,Price=price, Stock=stock, Color=color};
        //2.yöntem
        //Product newproduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };
        //clienttan validasyona uygun bir veri gelirse if'in içine girsin ve eklemeyi gerçekleştirsin.
        //if (!String.IsNullOrEmpty(newproduct.Name)&& newproduct.Name.StartsWith("A"))
        //{
        //    ModelState.AddModelError(string.Empty, "model ismi A harfi ile başlayamaz");
        //if (!String.IsNullOrEmpty(newproduct.Name) && newproduct.Name.StartsWith("A"))
        //{
        //    ModelState.AddModelError(string.Empty, "model ismi A harfi ile başlayamaz");
        //}

        
        if (ModelState.IsValid)
        {
            try
            {

                var product = _mapper.Map<Product>(newproduct);
                if (newproduct.Image != null && newproduct.Image.Length > 0)
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");//projemin root dosyasını verir.

                    var image = root.First(x => x.Name == "images");//wwwroot altındaki images dosyasına ulaşıyorum şu anda

                    var randomImageName = Guid.NewGuid() + Path.GetExtension(newproduct.Image.FileName);

                    var path = Path.Combine(image.PhysicalPath, randomImageName);

                    using var stream = new FileStream(path, FileMode.Create);//resmi kaydetmemiz için bir stream gerekli.FileMode.
                                                                             //create komutuda eğer böyle bir path yoksa oluştur demektir.

                    newproduct.Image.CopyTo(stream);//newproducttan gelen image streame aktarılsın.


                    
                    product.ImagePath = randomImageName;
                }
               



                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["status"] = "ürün başarıyla eklendi";
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ModelState.AddModelError(string.Empty, "Ürün kaydedilirken bir hata ile karşılaşıldı.Daha sonra tekrar deneyiniz");
                result = View();
            }
          
        }
        else
        {
       
            result = View();
        }

        var categories = _context.Category.ToList();
        ViewBag.categorySelect = new SelectList(categories, "Id", "Name");


        ViewBag.Expire = new Dictionary<string, int>()
        {
            {"1 Ay",1 },
            {"3 Ay",3 },
            {"6 Ay",6 },
            {"12 Ay",12 },

        };

        ViewBag.SelectColor = new SelectList(new List<ColorSelectList>(){
        new(){Data="Mavi",Value="Mavi"},
        new(){Data="Kırmızı",Value="Kırmızı"},
        new(){Data="Sarı",Value="Sarı"},
        }, "Value", "Data");
        return result;

    }
    //*/[ServiceFilter(typeof(NotFoundFilter))]*/
    [HttpGet]
    public IActionResult Update(int id) {
        var product = _context.Products.Find(id);

        var categories = _context.Category.ToList();
        ViewBag.categorySelect = new SelectList(categories, "Id", "Name",product.CategoryId);


        ViewBag.ExpireValue = product.Expire;
        ViewBag.Expire = new Dictionary<string, int>()
        {
            {"1 Ay",1 },
            {"3 Ay",3 },
            {"6 Ay",6 },
            {"12 Ay",12 },

        };

        ViewBag.SelectColor = new SelectList(new List<ColorSelectList>(){
        new(){Data="Mavi",Value="Mavi"},
        new(){Data="Kırmızı",Value="Kırmızı"},
        new(){Data="Sarı",Value="Sarı"},
        }, "Value", "Data", product.Color);


        return View(_mapper.Map<ProductUpdateViewModel>(product));
    }
    [HttpPost]
    public IActionResult Update(ProductUpdateViewModel updateProduct)
    {
        // var product = _context.Products.Find(Id);
        if (!ModelState.IsValid)
        {
            ViewBag.ExpireValue = updateProduct.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
        {
            {"1 Ay",1 },
            {"3 Ay",3 },
            {"6 Ay",6 },
            {"12 Ay",12 },

        };

            ViewBag.SelectColor = new SelectList(new List<ColorSelectList>(){
        new(){Data="Mavi",Value="Mavi"},
        new(){Data="Kırmızı",Value="Kırmızı"},
        new(){Data="Sarı",Value="Sarı"},
        }, "Value", "Data", updateProduct.Color);

            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name",updateProduct.CategoryId);

            return View();
        }
        if (updateProduct.Image != null && updateProduct.Image.Length > 0)
        {
            var root = _fileProvider.GetDirectoryContents("wwwroot");//projemin root dosyasını verir.

            var image = root.First(x => x.Name == "images");//wwwroot altındaki images dosyasına ulaşıyorum şu anda

            var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);

            var path = Path.Combine(image.PhysicalPath, randomImageName);

            using var stream = new FileStream(path, FileMode.Create);//resmi kaydetmemiz için bir stream gerekli.FileMode.
                                                                     //create komutuda eğer böyle bir path yoksa oluştur demektir.

            updateProduct.Image.CopyTo(stream);//newproducttan gelen image streame aktarılsın.

            updateProduct.ImagePath = randomImageName;
        }





        _context.Products.Update(_mapper.Map<Product>(updateProduct));
        _context.SaveChanges();
        TempData["status"] = "ürün başarıyla güncellendi";
        return RedirectToAction("Index");
    }
    [AcceptVerbs("GET","POST")]
    public IActionResult HasProductName(string Name)
    {
        var hasProduct= _context.Products.Any(x => x.Name == Name);
        if(hasProduct)
        {
            return Json("Kaydetmeye çalıştığınız ürün ismi veritabanında bulunmaktadır");
        }
        else
        {
            return Json(true);
        }
    }

} 
