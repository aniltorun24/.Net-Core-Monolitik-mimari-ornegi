namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Desciription { get; set; }
        public DateTime? PublichDate { get; set; }
        public string? Color {get; set; }
        public bool IsPublish { get; set; }
        public int Expire { get; set; }
        public string? ImagePath{ get; set; }
        public int CategoryId { get; set; }//one-to-many ilişkilerde many olan entity bir foreign key tutar.Bu da o keydir.
        //ef core böyle bir yazım yapınca one-to-many ilişki kurmak istediğini otomatik olarak anlar.
        public Category Category { get; set; }

    }
}
