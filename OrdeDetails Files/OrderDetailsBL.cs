using GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.DAL;
using Capgemini.GreatOutdoors.Exceptions;

namespace Capgemini.GreatOutdoors.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OrderDetailss from OrderDetailss collection.
    /// </summary>
    public class OrderDetailsBL : BLBase<OrderDetail>, IOrderDetailsBL, IDisposable
    {
        //fields
        OrderDetailsDALBase OrderDetailsDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OrderDetailsBL()
        {
            this.OrderDetailsDAL = new OrderDetailsDAL();
        }



        /// <summary>
        /// Adds new OrderDetails to OrderDetailss collection.
        /// </summary>
        /// <param name="newOrderDetails">Contains the OrderDetails details to be added.</param>
        /// <returns>Determinates whether the new OrderDetails is added.</returns>
        public async Task<bool> AddOrderDetailsBL(OrderDetail newOrderDetails)
        {
            bool OrderDetailsAdded = false;
            try
            {
                if (await Validate(newOrderDetails))
                {
                    await Task.Run(() =>
                    {
                        this.OrderDetailsDAL.AddOrderDetailsDAL(newOrderDetails);
                        OrderDetailsAdded = true;
                    });
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return OrderDetailsAdded;
        }



        /// <summary>
        /// Gets OrderDetails based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns OrderID object.</returns>
        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIDBL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrderDetails = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrderDetails = OrderDetailsDAL.GetOrderDetailsByOrderIDDAL(searchOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrderDetails;
        }
        /// <summary>
        /// Gets OrderDetail based on OrderDetailID.
        /// </summary>
        /// <param name="searchOrderDetailID">Represents OrderDetailID to search.</param>
        /// <returns>Returns OrderDetail object.</returns>
        public async Task<OrderDetail> GetOrderDetailByOrderDetailIDBL(Guid searchOrderDetailID)
        {
            OrderDetail matchingOrderDetail = new OrderDetail();
            try
            {
                await Task.Run(() =>
                {
                    matchingOrderDetail = OrderDetailsDAL.GetOrderDetailByOrderDetailIDDAL(searchOrderDetailID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrderDetail                               ;
        }

        /// <summary>
        /// Gets OrderDetails based on OrderDetailsName.
        /// </summary>
        /// <param name="searchProductID">Represents OrderDetailsName to search.</param>
        /// <returns>Returns OrderDetails object.</returns>
        public async Task<List<OrderDetail>> GetOrderDetailsByProductIDBL(Guid searchProductID)
        {
            List<OrderDetail> matchingOrderDetailss = new List<OrderDetail>();
            try
            {
                await Task.Run(() =>
                {
                    matchingOrderDetailss = OrderDetailsDAL.GetOrderDetailsByProductIDDAL(searchProductID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrderDetailss;
        }


        /// <summary>
        /// Deletes OrderDetails based on OrderDetailsID.
        /// </summary>
        /// <param name="deleteOrderID">Represents OrderDetailsID to delete.</param>
        /// /// <param name="deleteProductID">Represents OrderDetailsID to delete.</param>
        /// <returns>Determinates whether the existing OrderDetails is updated.</returns>
        public async Task<bool> DeleteOrderDetailsBL(Guid deleteOrderDetailID)
        {
            bool OrderDetailsDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    OrderDetailsDeleted = OrderDetailsDAL.DeleteOrderDetailsDAL(deleteOrderDetailID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return OrderDetailsDeleted;
        }


        /// <summary>
        /// Calculates the unit price after product discount is applied.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns> Returns the final unit price.</returns>
        public async Task<decimal> CalculateDiscountPriceBL(Guid productID)
        {
            ProductBL Access = new ProductBL();
            Product findProduct = await Access.GetProductByProductIDBL(productID);
            decimal DiscountedPrice = (decimal)(findProduct.CostPrice * (1 - (findProduct.DiscountPercentage / 100)));
            return DiscountedPrice;
        }

        /// <summary>
        /// Calculate the total price of each product.
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns>Returns Each product's total price.</returns>

        public decimal CalculateTotalPriceBL(OrderDetail orderDetail)
        {
            decimal TotalPrice = (orderDetail.Quantity * orderDetail.DiscountedUnitPrice);
            return TotalPrice;
        }


        /// <summary>
        /// Calculate Amount Payable.
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>Return total amount to be paid.</returns>
        public async Task<decimal> AmountPayable(List<OrderDetail> orderDetails)
        {
            decimal amountPayable = 0;
            await Task.Run(() =>
            {
                foreach (OrderDetail item in orderDetails)
                {
                    amountPayable = item.TotalPrice + amountPayable;
                }
            });

            return amountPayable;
        }

        /// <summary>
        /// Calculate total products that are going to be ordered..
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>Return total quantity that will be shipped.</returns>
        public async Task<int> TotalQuantity(List<OrderDetail> orderDetails)
        {
            int totalQuantity = 0;
            await Task.Run(() =>
            {
                foreach (OrderDetail item in orderDetails)
                {
                    totalQuantity = item.Quantity + totalQuantity;
                }
            });

            return totalQuantity;
        }
        OrderDetailsDAL orderDetailDAL;
        public bool UpdateOrderDetailStatusBL(Guid orderId, string statusChange)
        {
            bool statusUpdated = false;
            try
            {
                
                bool dalUpdated = OrderDetailsDAL.UpdateOrderDetailDAL(orderId, statusChange);
                if (dalUpdated == true)
                    statusUpdated = true;
                return statusUpdated;
         }
            catch (GreatOutdoorsException ex)
            {
                throw ex;
            }
        }

        public bool UpdateOrderDetailForReturn(Guid orderDetailId,string statusChange)
        {
            bool isChanged = false;
            try
            {
                bool updated = OrderDetailsDAL.UpdateOrderDetailForReturn(orderDetailId,statusChange);
                return isChanged;
            }
            catch (GreatOutdoorsException ex){ throw ex; }

        }



        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OrderDetailsDAL)OrderDetailsDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public static void Serialize()
        {
            try
            {
                //OrderDetailsDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///Invokes Deserialize method of DAL.
        /// </summary>
        public static void Deserialize()
        {
            try
            {
                // OrderDetailsDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
