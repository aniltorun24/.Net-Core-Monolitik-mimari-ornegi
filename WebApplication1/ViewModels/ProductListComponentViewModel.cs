namespace WebApplication1.ViewModels;

public class ProductListComponentViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
//view componentların partial viewslardan farkı business kodlarını kendi içinde bulundurması bu sayede ek kod yazımını engelliyor
//ikinci olarak da partial view bir cshtml dosyasına sahip olabilirken view componentlar birden fazla cshtm dosyasına sahip olabilir.