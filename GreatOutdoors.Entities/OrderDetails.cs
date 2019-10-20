using Capgemini.GreatOutdoors.Helpers.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.Entities
{
    public enum ProductStatus
    {
        InCart, UnderProcessing, Shipped, Delivered, Return, Cancel
    }

    /// <summary>
    /// Interface for Order Details Entity
    /// </summary>
    public interface IOrderDetails
    {
        Guid OrderDetailID { get; set; }
        Guid OrderID { get; set; }
        Guid ProductID { get; set; }
        int Quantity { get; set; }
        double UnitPrice { get; set; }
        double TotalPrice { get; set; }
        Guid AddressID { get; set; }
    }

    /// <summary>
    /// Represents Order Details
    /// </summary>
    public class OrderDetail : IOrderDetails
    {
        /* Auto-Implemented Properties */
        [Required("OrderDetail ID can't be blank.")]
        public Guid OrderDetailID { get; set; }

        [Required("Order ID can't be blank.")]
        public Guid OrderID { get; set; }

        [Required("Product ID can't be blank.")]
        public Guid ProductID { get; set; }

        [Required("Quantity can't be blank.")]
        public int Quantity { get; set; }

        [Required("Unit price can't be blank.")]
        public double UnitPrice { get; set; }

        [Required("Total Price can't be blank.")]
        public double TotalPrice { get; set; }
        public Guid AddressID { get; set; }
        public ProductStatus Status { get; set; }

        /* Constructor */
        public OrderDetail()
        {
            OrderDetailID = default(Guid);
            OrderID = default(Guid);
            ProductID = default(Guid);
            Quantity = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            Status = ProductStatus.InCart;
            AddressID = default(Guid);
        }
    }
}
