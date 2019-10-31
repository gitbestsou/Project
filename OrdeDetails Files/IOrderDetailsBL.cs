using GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.Contracts.BLContracts
{
    public interface IOrderDetailsBL : IDisposable
    {
        Task<bool> AddOrderDetailsBL(OrderDetail newOrderDetails);
        Task<OrderDetail> GetOrderDetailByOrderDetailIDBL(Guid searchOrderDetailID);
        Task<List<OrderDetail>> GetOrderDetailsByOrderIDBL(Guid searchOrderID);

        Task<List<OrderDetail>> GetOrderDetailsByProductIDBL(Guid searchProductID);

        Task<bool> DeleteOrderDetailsBL(Guid deleteOrderDetailID);

        Task<decimal> CalculateDiscountPriceBL(Guid productID);


        decimal CalculateTotalPriceBL(OrderDetail orderDetails);

        Task<int> TotalQuantity(List<OrderDetail> orderDetails);

        Task<decimal> AmountPayable(List<OrderDetail> orderDetails);
        bool UpdateOrderDetailStatusBL(Guid orderId, string statusChange);
        bool UpdateOrderDetailForReturn(Guid orderId,string statusChange);
        //bool UpdateOrderDetailStatusBL(Guid orderId, Guid productId, ProductStatus statusChange);

    }
}
