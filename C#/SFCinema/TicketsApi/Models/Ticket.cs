using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsApi.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Movie { get; set; }
        public decimal Price { get; set; }
        public int TicketsLeft { get; set; }
    }
}
