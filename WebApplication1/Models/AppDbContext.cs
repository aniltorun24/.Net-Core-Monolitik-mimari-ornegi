using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<WebApplication1.Models.Category>? Category { get; set; }
    }
}
