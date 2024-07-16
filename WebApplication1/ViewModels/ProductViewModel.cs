using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    [Remote(action:"HasProductName",controller:"Products")]
    [StringLength(50,ErrorMessage ="İsim alanına en fazla 50 karekter girilebilir")]
    [Required(ErrorMessage ="İsim kısmı boş olamaz")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Fiyat kısmı boş olamaz")]
    [Range(1, 200, ErrorMessage = "Price alanı 1 ile 1000 arası bir değer olmalıdır.")]
    public decimal? Price { get; set; }
    [Required(ErrorMessage = "Stok kısmı boş olamaz")]
    [Range(1,200,ErrorMessage = "Stok alanı 1 ile 200 arası bir değer olmalıdır.")]
    public int? Stock { get; set; }
    [Required(ErrorMessage = "Açıklama kısmı boş olamaz")]
    [StringLength(300,MinimumLength =50, ErrorMessage = "İsim alanı 50 ie 300 karekter arasında olmalıdır.")]
    public string Desciription { get; set; }
    [Required(ErrorMessage = "Yayınlanma tarihi kısmı boş olamaz")]
    public DateTime? PublichDate { get; set; }
    [Required(ErrorMessage = "Renk seçiniz")]
    public string? Color { get; set; }
    public bool IsPublish { get; set; }
    [Required(ErrorMessage = "Yayınlanma süresi  boş olamaz")]
    public int? Expire { get; set; }
    //[EmailAddress(ErrorMessage ="Email adresi uygun formatta değil.")]
    //public int EmailAdress { get; set; }
    [ValidateNever]
    public IFormFile? Image { get; set; }

    [ValidateNever]
    public string? ImagePath { get; set; }

    [Required(ErrorMessage = "Kategori seçiniz")]
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
}
