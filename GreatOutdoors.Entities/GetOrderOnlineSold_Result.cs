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
    
    public partial class GetOrderOnlineSold_Result
    {
        public System.Guid OrderID { get; set; }
        public Nullable<System.Guid> RetailerID { get; set; }
        public Nullable<System.Guid> SalespersonID { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ChannelOfSale { get; set; }
        public Nullable<System.DateTime> OrderDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
    }
}