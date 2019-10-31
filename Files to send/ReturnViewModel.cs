using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GreatOutdoors.Mvc.Models
{
    public class ReturnViewModel
    {
        public System.Guid ReturnID { get; set; }
        public System.Guid ReturnDetailID { get; set; }
        public System.Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ReturnDateTime { get; set; }


        //[Required(ErrorMessage = "Reason of Return cannot be blank")]
        public string ReasonOfReturn { get; set; }





        public decimal TotalAmount { get; set; }

        //[Required(ErrorMessage = "Feedback cannot be blank")]
        //[RegularExpression("^[A-Za-z]*$", ErrorMessage = "Feedback should contain alphabets only")]
        //public string Feedback { get; set; }
        //public bool variable { get; set; }
        //public string dropdown { get; set; }


    }






}