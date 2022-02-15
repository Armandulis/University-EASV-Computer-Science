using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastucture.Data
{
    public class InitialiseDB
    {
        public static void SeedDB(PetShopContext ctx){

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var owner3 = ctx.Owner.Add(new Owner()
            {
                Address = "asd",
                Email = "asd@Gmakil",
                FirstName = "Patric",
                LastName = "no",
                PhoneNumber = "This is patric"

            }).Entity;
            var owner2 = ctx.Owner.Add(new Owner()
            {
                Address = "asd",
                Email = "asd@Gmakil",
                FirstName = "Obama",
                LastName = "michel",
                PhoneNumber = "9486"
            }).Entity;
            var owner1 = ctx.Owner.Add(new Owner()
            {
                Address = "Big M",
                Email = "Big m@Gmakil",
                FirstName = "Big",
                LastName = "M",
                PhoneNumber = "4520"

            }).Entity;
            ctx.Pet.Add(new Pet()
            {
                Name = "Ducky",
                Color = "White/Black",
                Price = 99.69,
                PreviousOwner = owner1,
                Type = "Duck"




            });
            ctx.Pet.Add(new Pet()
            {
                Name = "Katoo",
                Color = "Ginger",
                Price = 99.69,
                PreviousOwner = owner2,
                Type = "cat"



            });
            ctx.Pet.Add(new Pet()
            {
                Name = "Lmao",
                Color = "Any",
                Price = 99.69,
                PreviousOwner = owner2,
                Type = "Doh"


            });
            

            var password = "Password";

            byte[] passwordHashA, passwordSaltA, passwordHashAdmin, passwordSaltAdmin;

            CreatePasswordHash(password, out passwordHashA, out passwordSaltA);
            CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);

            ctx.User.Add(new User()
            {
                Username = "Armandas",
                PasswordHash = passwordHashA,
                PasswordSalt = passwordSaltA,
                isAdmin = false
            });

            ctx.User.Add(new User()
            {
                Username = "Admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin    ,
                isAdmin = true
            });

            ctx.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
