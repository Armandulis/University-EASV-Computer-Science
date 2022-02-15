using Microsoft.EntityFrameworkCore;
using SharedModels;
using DBContext = Microsoft.EntityFrameworkCore.DbContext;

namespace CustomerAPI.Data
{
    public class CustomerAPIContext : DBContext
    {
        public CustomerAPIContext(DbContextOptions<CustomerAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
