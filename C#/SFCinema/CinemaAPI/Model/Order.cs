using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Movie { get; set; }
        public int TicketQuantity { get; set; }
        public string Food { get; set; }
        public int FoodQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
