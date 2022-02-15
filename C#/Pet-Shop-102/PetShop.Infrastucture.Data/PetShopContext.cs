using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastucture.Data
{
    public class PetShopContext :DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> option) : base(option){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pet>().HasOne(p => p.PreviousOwner).WithMany(o => o.OwnedPets).OnDelete(DeleteBehavior.SetNull);
        }


        public DbSet<Owner> Owner { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<User> User { get; set; }
    }
}
