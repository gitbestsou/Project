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
    Last modified : 08/11/2019
    */
    public class OrderDetailViewModel
    {
        //Creating the necessary properties for OrderDetails,PlaceReturn,RequestCancel view
        public System.Guid OrderDetailID { get; set; }
        public System.Guid OrderID { get; set; }
        public System.Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountedUnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public System.Guid AddressID { get; set; }
        public string CurrentStatus { get; set; }



        public string ProductName { get; set; }



        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string MobileNo { get; set; }

        public string[] Reasons = { "Wrong", "Incomplete" };

        [Required(ErrorMessage = "Reason of Return cannot be blank")]
        public string ReasonOfReturn { get; set; }
    }
}