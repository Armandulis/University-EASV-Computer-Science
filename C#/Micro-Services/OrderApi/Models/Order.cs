using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
    public class Order
    {
        public enum Status
        {
            Completed = 1,
            Cancelled = 2,
            Shipped = 3,
            Paid = 4
        };
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public List<ProductDTO> Products { get; set; }
        public int CustomerID { get; set; }
        public Status StatusCode { get; set; }
        public OrderLine[] OrderLines { get; set; }
    }
}
