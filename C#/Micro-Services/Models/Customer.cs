using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public bool CreditStanding { get; set; }
    }
}
