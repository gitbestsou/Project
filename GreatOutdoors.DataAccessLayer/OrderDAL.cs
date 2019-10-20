using System;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Helpers;
using System.Collections.Generic;
using Capgemini.GreatOutdoors.Exceptions;

namespace Capgemini.GreatOutdoors.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting order from Orders collection.
    /// </summary>
    public class OrderDAL : OrdersDALBase, IDisposable
    {
        /// <summary>
        /// Adds new order to Orders collection.
        /// </summary>
        /// <param name="newOrder">Contains the Order details to be added.</param>
        /// <returns>Determinates whether the new Order is added.</returns>

        public override (bool, Guid) AddOrderDAL(Order newOrder)
        {
            bool orderAdded = false;
            Guid orderID = default(Guid);
            try
            {
                newOrder.OrderID = Guid.NewGuid();
                orderList.Add(newOrder);
                orderID = newOrder.OrderID;
                                orderAdded = true;
            }
            catch (GreatOutdoorsException ex)
            {
                throw new Exception(ex.Message);
            }
            return (orderAdded, orderID);

        }

        /// <summary>
        /// Gets all orders from the collection.
        /// </summary>
        /// <returns>Returns list of all orders.</returns>
        public override List<Order> GetAllOrdersDAL()
        {
            return orderList;
        }

        /// <summary>
        /// Gets order based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns Order object.</returns>
        public override Order GetOrderByOrderIDDAL(Guid searchOrderID)
        {
            Order searchOrder = null;
            try
            {
                foreach (Order item in orderList)
                {
                    if (item.OrderID == searchOrderID)
                    {
                        searchOrder = item;
                    }
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return searchOrder;
        }

        /// <summary>
        /// Gets the orders filtered by retailer ID
        /// </summary>
        /// <param name="RetailerID"></param>
        /// <returns>Returns list of orders.</returns>
        public override List<Order> GetOrderByRetailerIDDAL(Guid RetailerID)
        {
            List<Order> searchOrder = new List<Order>();
            try
            {
                foreach (Order item in orderList)
                {
                    if (item.RetailerID == RetailerID)
                    {
                        searchOrder.Add(item);
                    }
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return searchOrder;
        }



        /// <summary>
        /// Searches for the orders placed by a particular salesman by using the salesman ID.
        /// </summary>
        /// <param name="salesPersonID"></param>
        /// <returns>the list of orders.</returns>
        public override List<Order> GetOrderBySalesPersonIDDAL(Guid salesPersonID)
        {
            List<Order> searchOrder = new List<Order>();
            try
            {
                foreach (Order item in orderList)
                {
                    if (item.SalesPersonID == salesPersonID)
                    {
                        searchOrder.Add(item);
                    }
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return searchOrder;
        }



        /// <summary>
        /// Returns a list of orders that are to be sold offline
        /// </summary>
        /// <returns>List of all offline orders</returns>
        public override List<Order> GetOrderForOfflineSaleDAL()
        {
            List<Order> searchOrder = new List<Order>();
            try
            {
                searchOrder = orderList.FindAll(
                    (item) => { return item.ChannelOfSale == Channel.Offline /*&& item.CurrentStatus == Status.InCart;*/; }
                );
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return searchOrder;
        }

        /// <summary>
        /// Returns list of online orders
        /// </summary>
        /// <returns> List of online orders</returns>
        public override List<Order> GetOrderOnlineDAL()
        {
            List<Order> searchOrder = new List<Order>();
            try
            {
                searchOrder = orderList.FindAll(
                    (item) => { return item.ChannelOfSale == Channel.Online; }
                );
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return searchOrder;
        }

        /// <summary>
        /// Returns list of orders by status
        /// </summary>
        /// <param name="CurrentStatus"></param>
        /// <returns>List of orders with the given status</returns>

        //public override List<Order> GetOrderByStatusDAL(Status CurrentStatus)
        //{
        //    List<Order> searchOrder = new List<Order>();
        //    try
        //    {
        //        foreach (Order item in orderList)
        //        {
        //            if (item.CurrentStatus == CurrentStatus)
        //            {
        //                searchOrder.Add(item);
        //            }
        //        }
        //    }
        //    catch (GreatOutdoorsException ex)
        //    {
        //        throw new GreatOutdoorsException(ex.Message);
        //    }
        //    return searchOrder;
        //}

        /// <summary>
        /// Update address of the order
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        public override bool UpdateOrderDAL(Order updateOrder)
        {
            bool OrderUpdated = false;
            try
            {
                //Find SalesPerson based on SalesPersonID
                Order matchingOrder = GetOrderByOrderIDDAL(updateOrder.OrderID);

                if (matchingOrder != null)
                {
                    //Update SalesPerson details
                    ReflectionHelpers.CopyProperties(updateOrder, matchingOrder, new List<string>() { "AddressID" });

                    OrderUpdated = true;
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw  new GreatOutdoorsException(ex.Message);
            }

            return OrderUpdated;

        }

        public override bool UpdateOrderStatusDAL(Order updateOrder)
        {
            bool OrderUpdated = false;
            try
            {
                //Find SalesPerson based on SalesPersonID
                Order matchingOrder = GetOrderByOrderIDDAL(updateOrder.OrderID);

                if (matchingOrder != null)
                {
                    //Update SalesPerson details
                    ReflectionHelpers.CopyProperties(updateOrder, matchingOrder, new List<string>() { "CurrentStatus" });

                    OrderUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }

            return OrderUpdated;

        }

        public override bool DeleteOrderByOrderIDDAL(Guid orderID)
        {
            bool orderDeleted = false;
            try
            {
                //Find SalesPerson based on searchSalesPersonID
               Order matchingOrder = orderList.Find(
                    (item) => { return item.OrderID == orderID; }
                );

                if (matchingOrder != null)
                {
                    //Delete SalesPerson from the collection
                    orderList.Remove(matchingOrder);
                    orderDeleted = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return orderDeleted;
        }
        public void Dispose()
        {
            //No unmanaged resources currently
        }


    }
}
