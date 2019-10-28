using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatOutdoors.MVC.Models
{
    public class OrderDetailViewModel
    {
        public System.Guid OrderDetailID { get; set; }
        public System.Guid OrderID { get; set; }
        public System.Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountedUnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public System.Guid AddressID { get; set; }
        public string CurrentStatus { get; set; }
    }
}