using Microsoft.EntityFrameworkCore;
using OrderServiceApi.Models;

namespace OrderServiceApi.Data
{
    public class OrderApiContext : DbContext
    {
        public OrderApiContext(DbContextOptions<OrderApiContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
