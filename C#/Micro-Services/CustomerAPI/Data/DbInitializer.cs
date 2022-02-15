using SharedModels;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Data
{
    public class DbInitializer : IDbInitializer
    {
        void IDbInitializer.Initializer(CustomerAPIContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                // Check if DB has been seeded.
                return;
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer { ID = 1, Name = "Armandas", Email = "Armandas@gmail.com", Phone = "+378451351", BillingAddress = "Havnegade 62", ShippingAddress = "Molevej 123", CreditStanding = true},
                new Customer { ID = 2, Name = "Emil", Email = "Emil@hotmail.dk", Phone = "+895218152", BillingAddress = "Molevej 98", ShippingAddress = "Molevej AB9", CreditStanding = false},
                new Customer { ID = 3, Name = "RANDOMArmandas", Email = "RANDOMArmandas@gmail.com", Phone = "+323423478451351", BillingAddress = "RANDOMHavnegade 62", ShippingAddress = "RANDOMMolevej 123", CreditStanding = true},
                new Customer { ID = 4, Name = "RANDOMEmil", Email = "RANDOMEmil@hotmail.dk", Phone = "+23423895218152", BillingAddress = "RANDOMMolevej 98", ShippingAddress = "RANDOMMolevej AB9", CreditStanding = false}

            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
