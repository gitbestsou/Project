using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GreatOutdoors.Entities;
namespace GreatOutdoors.Mvc.Models
{

    /*Order ViewModel
    Project name : Great Outdoors
    Developer name: Sourav Maji
    Use case : Return
    Creation date : 30/10/2019
    Last modified : 07/11/2019
    */
    public class OrderViewModel
    {
        //Creating the necessary properties for OrderHistory View
        public System.Guid OrderID { get; set; }
        public System.Guid RetailerID { get; set; }
        public System.Guid SalespersonID { get; set; }
        public string RetailerName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ChannelOfSale { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}