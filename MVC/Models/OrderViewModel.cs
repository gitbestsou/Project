using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GreatOutdoors.Entities;
namespace GreatOutdoors.Mvc.Models
{
    public class OrderViewModel
    {
        public System.Guid OrderID { get; set; }
        public System.Guid RetailerID { get; set; }
        public System.Guid SalespersonID { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ChannelOfSale { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}