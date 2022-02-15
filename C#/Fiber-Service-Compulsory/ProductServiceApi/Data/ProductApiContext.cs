using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Models;

namespace ProductServiceApi.Data
{
    public class ProductApiContext : DbContext
    {
        public ProductApiContext(DbContextOptions<ProductApiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
