using FoodApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApi.Infrastructure
{
    public class DBContext : DbContext
    {
        public DBContext(string connectionString) : base(connectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public IDbSet<Food> Food { get; set; }

    }
}
