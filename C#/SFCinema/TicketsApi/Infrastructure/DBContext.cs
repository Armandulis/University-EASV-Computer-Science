using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TicketsApi.Models;

namespace TicketsApi.Infrastructure
{
    public class DBContext : DbContext
    {
        public DBContext(string connectionString) : base(connectionString)
        {

        }

        public IDbSet<Ticket> Tickets { get; set; }

    }
}
