namespace WebApplication1.Models
{
    public class ProductRepository
    {
        private static List<Product> _product = new List<Product>(){
        new () { Id = 1, Name = "Kalem1", Price = 100, Stock = 200 },
        new () { Id = 2, Name = "Kalem2", Price = 200, Stock = 300 },
        new() { Id = 3, Name = "Kalem3", Price = 300, Stock = 400 }
    };



public List<Product> GetAll() => _product;

        public void add(Product newProduct) => _product.Add(newProduct);

        public void remove(int id)
        {
            var hasProduct = _product.FirstOrDefault(x => x.Id == id);

            if (hasProduct == null)
            {
                throw new Exception($"bu id({id})'ye sahip ürün bulunamadı");
            }
            _product.Remove(hasProduct);
        }
        public void update(Product updateProduct)
        {
            var hasProduct = _product.FirstOrDefault(x => x.Id == updateProduct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"bu id({updateProduct.Id})'ye sahip ürün bulunamadı");
            }
            hasProduct.Name = updateProduct.Name;
            hasProduct.Price = updateProduct.Price;
            hasProduct.Stock = updateProduct.Stock;

            var Index = _product.FindIndex(x => x.Id == updateProduct.Id);
            _product[Index] = hasProduct;
        }
    }
    }

