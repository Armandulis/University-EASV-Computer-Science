using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float PriceForAll { get; set; }
    }
}
