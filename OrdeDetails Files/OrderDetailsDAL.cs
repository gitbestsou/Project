using Capgemini.GreatOutdoors.Contracts.DALContracts;
using GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Capgemini.GreatOutdoors.DataAccessLayer;
using Capgemini.GreatOutdoors.Exceptions;

namespace Capgemini.GreatOutdoors.DAL
{

    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OrderDetailss from OrderDetailss collection.
    /// </summary>
    public class OrderDetailsDAL : OrderDetailsDALBase
    {

        TeamAEntities entities = new TeamAEntities();
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
                entities.AddOrderDetails(newOrderDetails.OrderDetailID, newOrderDetails.OrderID, newOrderDetails.ProductID, newOrderDetails.Quantity, newOrderDetails.DiscountedUnitPrice, newOrderDetails.TotalPrice, newOrderDetails.GiftPacking, newOrderDetails.AddressID);
                OrderDetailsAdded = true;

            }
            catch (GreatOutdoorsException ex)
            {
                throw ex;
            }


            //newOrderDetails.OrderDetailID = Guid.NewGuid();
            //newOrderDetails.Status = ProductStatus.InCart;
            //OrderDetailsList.Add(newOrderDetails);
            //OrderDetailsAdded = true;

            return OrderDetailsAdded;
        }


        public override List<OrderDetail> GetAllOrderDetailsDAL()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            try
            {
                orderDetails = entities.OrderDetails.ToList();



            }

            catch (GreatOutdoorsException ex)
            {
                throw ex;
            }


            return orderDetails;
        }

        /// <summary>
        /// Gets OrderDetails based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderDetailsID to search.</param>
        /// <returns>Returns List of OrderDetails object.</returns>
        public override List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID)
        {


            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                orderDetails = entities.GetOrderDetailsByOrderID(searchOrderID).ToList();




            }
            catch (GreatOutdoorsException)
            {
                throw;
            }


            return orderDetails;
        }

        public override OrderDetail GetOrderDetailByOrderDetailIDDAL(Guid searchOrderDetailID)
        {
            OrderDetail orderDetail = new OrderDetail();
            try
            {
                orderDetail = entities.GetOrderDetailByOrderDetailID(searchOrderDetailID).ToList()[0];
            }
            catch (GreatOutdoorsException)
            {

            }
            return orderDetail;
        }

        //List<OrderDetail> matchingOrderDetails = null;
        //try
        //{
        //    //Find OrderDetails based on searchOrderDetailsID
        //    matchingOrderDetails = OrderDetailsList.FindAll(
        //        (item) => { return item.OrderID == searchOrderID; }
        //    );

        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        //return matchingOrderDetails;


        //public override List<OrderDetail> GetAllOrderDetailsDAL()
        //{
        //    return OrderDetailsList;
        //}

        /// <summary>
        /// Gets OrderDetails based on ProductID.
        /// </summary>
        /// <param name="searchProductID">Represents OrderDetailsID to search.</param>
        /// <returns>Returns OrderDetails object.</returns>
        //public override List<OrderDetail> GetOrderDetailsByProductIDDAL(Guid searchProductID)
        //{
        //    List<OrderDetail> matchingOrderDetails = null;
        //    try
        //    {
        //        //Find OrderDetails based on searchOrderDetailsID
        //        matchingOrderDetails = OrderDetailsList.FindAll(
        //            (item) => { return item.OrderDetailID == searchProductID; }
        //        );
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return matchingOrderDetails;
        //}

        public override List<OrderDetail> GetOrderDetailsByProductIDDAL(Guid searchProductID)
        {
            List<OrderDetail> matchingOrderDetails = null;
            try
            {
                matchingOrderDetails = entities.GetOrderDetailsByProductID(searchProductID).ToList();

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
        public override bool DeleteOrderDetailsDAL(Guid deleteOrderDetailID)
        {
            bool orderDeleted = false;
            try
            {
                entities.DeleteOrderDetails(deleteOrderDetailID);
                entities.SaveChanges();
                orderDeleted = true;
            }
            catch (Exception)
            {
                throw;
            }

            return orderDeleted;
        }


        public override bool DeleteOrderDetailsByOrderID(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                entities.DeleteOrderDetailsByOrderID(deleteOrderID);
                entities.SaveChanges();
                orderDeleted = true;
            }
            catch (Exception)
            {
                throw;
            }

            return orderDeleted;
        }

        public override bool UpdateOrderDetailDAL(Guid orderId, string statusChange)
        {
            bool updateOrderDetailStatus = false;
            try
            {

                entities.UpdateOrderDetail(orderId, statusChange);
                entities.SaveChanges();
                updateOrderDetailStatus = true;
            }
            catch (Exception)
            {
                throw;
            }

            return updateOrderDetailStatus;

        }
        public override bool UpdateOrderDetailForReturn(Guid orderDetailId, string statusChange)
        {
            bool isChanged = false;
            try
            {
                entities.UpdateOrderDetailForReturn(orderDetailId, statusChange);
                entities.SaveChanges();
                isChanged = true;
            }
            catch (Exception)
            {
                throw;
            }

            return isChanged;
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
