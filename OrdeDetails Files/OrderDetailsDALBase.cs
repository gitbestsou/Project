using System;
using System.Collections.Generic;
using System.IO;
using GreatOutdoors.Entities;
using Newtonsoft.Json;

namespace Capgemini.GreatOutdoors.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for OrderDetailsDAL class
    /// </summary>
    public abstract class OrderDetailsDALBase
    {
        //Collection of OrderDetails
        protected static List<OrderDetail> OrderDetailsList = new List<OrderDetail>();
        

        //Methods for CRUD operations
        public abstract bool AddOrderDetailsDAL(OrderDetail newOrderDetails);
        public abstract OrderDetail GetOrderDetailByOrderDetailIDDAL(Guid searchOrderDetailID);
        public abstract List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID);
        public abstract List<OrderDetail> GetOrderDetailsByProductIDDAL(Guid searchProductID);
        public abstract bool DeleteOrderDetailsDAL(Guid deleteOrderDetailID);
        public abstract bool DeleteOrderDetailsByOrderID(Guid deleteOrderID);




        public abstract List<OrderDetail> GetAllOrderDetailsDAL();
        public abstract bool UpdateOrderDetailDAL(Guid orderId, string statusChange);
        public abstract bool UpdateOrderDetailForReturn(Guid orderDetailId, string statusChange);


        

    }
}
