using CinemaAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure
{
    public interface ICinemaRepository
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);
        Order Add(Order order);
        void Edit(Order order);
        void Remove(int id);

    }
}
