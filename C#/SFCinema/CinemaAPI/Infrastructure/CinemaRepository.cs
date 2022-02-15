using CinemaAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaApiContext db;
        public CinemaRepository()
        {

            db = new CinemaApiContext("Server=tcp:sfsynopsis.database.windows.net,1433;Initial Catalog=SFSynopsis;Persist Security Info=False;User ID=armandas;Password=SFCinema1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }

        public Order Add(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order;
        }


        public void Edit(Order order)
        {
            // Edits order
            db.Orders.Add(order);
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
        }

        public Order Get(int id)
        {
            return db.Orders.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(int id)
        {
            db.Orders.Remove(Get(id));
            db.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders.ToList();
        }
    }
}
