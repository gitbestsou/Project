using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GreatOutdoors.MVC.Models
{
    public class ReturnViewModel
    {
        public System.Guid ReturnID { get; set; }


        

        [Required(ErrorMessage = "Reason of Return cannot be blank")]
        public string ReasonOfReturn { get; set; }


        
        
        
        public decimal TotalAmount { get; set; }
        
        [Required(ErrorMessage = "Feedback cannot be blank")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Feedback should contain alphabets only")]
        public string Feedback { get; set; }
        public bool variable { get; set; }
        public string dropdown { get; set; }


    }
    
    
       


        
}