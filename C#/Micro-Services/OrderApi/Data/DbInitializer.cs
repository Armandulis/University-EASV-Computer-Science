using System.Collections.Generic;
using System.Linq;
using System;
using SharedModels;

namespace OrderApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            List<ProductDTO> productsOne = new List<ProductDTO>
            {
                new ProductDTO { OrderId = 1, ProductId = 1, PriceForAll = 4, Quantity = 1}
            };
            List<ProductDTO> productsTwo = new List<ProductDTO>
            {
                new ProductDTO { OrderId = 2, ProductId = 2, PriceForAll = 4, Quantity = 2},
                new ProductDTO { OrderId = 3, ProductId = 3, PriceForAll = 4, Quantity = 2}
            };
            List<ProductDTO> productsThree = new List<ProductDTO>
            {
                new ProductDTO { OrderId = 4, ProductId = 2, PriceForAll = 6, Quantity = 2},
                new ProductDTO { OrderId = 5, ProductId = 3, PriceForAll = 7, Quantity = 2}
            };
            List<Order> orders = new List<Order>
            {
                new Order { Date = DateTime.Today, CustomerID = 1, Products = productsOne},
                new Order {Date = DateTime.Today, CustomerID = 2, Products = productsTwo},
                new Order { Date = DateTime.Today, CustomerID = 1, Products = productsThree},
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
