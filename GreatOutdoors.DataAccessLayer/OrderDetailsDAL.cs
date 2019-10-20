using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.DAL
{

    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OrderDetailss from OrderDetailss collection.
    /// </summary>
    public class OrderDetailsDAL : OrderDetailsDALBase, IDisposable
    {
        /// <summary>
        /// Adds new OrderDetails to OrderDetails collection.
        /// </summary>
        /// <param name="newOrderDetails">Contains the OrderDetails details to be added.</param>
        /// <returns>Determinates whether the new OrderDetails is added.</returns>
        public override bool AddOrderDetailsDAL(OrderDetail newOrderDetails)
        {
            bool OrderDetailsAdded = false;
            try
            {
                newOrderDetails.OrderDetailID = Guid.NewGuid();
                newOrderDetails.Status = ProductStatus.InCart;
                OrderDetailsList.Add(newOrderDetails);
                OrderDetailsAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return OrderDetailsAdded;
        }


        /// <summary>
        /// Gets OrderDetails based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderDetailsID to search.</param>
        /// <returns>Returns List of OrderDetails object.</returns>
        public override List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrderDetails = null;
            try
            {
                //Find OrderDetails based on searchOrderDetailsID
                matchingOrderDetails = OrderDetailsList.FindAll(
                    (item) => { return item.OrderID == searchOrderID; }
                );
                
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrderDetails;
        }


        public override List<OrderDetail> GetAllOrderDetailsDAL()
        {
            return OrderDetailsList;
        }

        /// <summary>
        /// Gets OrderDetails based on ProductID.
        /// </summary>
        /// <param name="searchProductID">Represents OrderDetailsID to search.</param>
        /// <returns>Returns OrderDetails object.</returns>
        public override List<OrderDetail> GetOrderDetailsByProductIDDAL(Guid searchProductID)
        {
            List<OrderDetail> matchingOrderDetails = null;
            try
            {
                //Find OrderDetails based on searchOrderDetailsID
                matchingOrderDetails = OrderDetailsList.FindAll(
                    (item) => { return item.ProductID == searchProductID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrderDetails;
        }



        /// <summary>
        /// Deletes OrderDetails based on OrderID and ProductID.
        /// </summary>
        /// <param name="deleteOrderID">Represents OrderDetailsID to delete.</param>
        /// <param name="deleteProductID">Represents OrderDetailsID to delete.</param>
        /// <returns>Determinates whether the existing OrderDetails is updated.</returns>
        public override bool DeleteOrderDetailsDAL(Guid deleteOrderID, Guid deleteProductID)
        {
            bool orderDeleted = false;
            try
            {

                OrderDetail matchingOrderDetails = OrderDetailsList.Find(
                    (item) => { return item.OrderDetailID == deleteOrderID && item.ProductID == deleteProductID; }
                );

                if (matchingOrderDetails != null)
                {
                    //Delete OrderDetails from the collection
                    OrderDetailsList.Remove(matchingOrderDetails);
                    orderDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }

        public override bool UpdateOrderDetailStatusDAL(Guid orderId, Guid productId, ProductStatus statusChange)
        {
            bool updateOrderDetailStatus = false;
            try
            {
                foreach (var item in OrderDetailsList)
                {
                    if (item.OrderID == orderId && item.ProductID == productId)
                        item.Status = statusChange;
                    updateOrderDetailStatus = true;
                       
                }
               
              
            }
            catch (Exception)
            {
                throw;
            }
            return updateOrderDetailStatus;
        }




        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}

