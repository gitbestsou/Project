//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreatOutdoors.Entities
{
    using System;
    
    public partial class GetProductsByCategory_Result
    {
        public System.Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> Stock { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string TechnicalSpecifications { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
    }
}
