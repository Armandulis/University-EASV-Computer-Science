using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApi.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string FoodName  { get; set; }
        public int AmountLeft { get; set; }
    }
}
