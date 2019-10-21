using Capgemini.GreatOutdoors.BusinessLayer;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.DataAccessLayer;
using GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.PresentationLayer
{
    public static class CancelOrder
    {
        public static async Task CancelRequest(Guid currUserID,Helpers.UserType userType)
        {
            try
            {
                using (IOrdersBL orderAccess = new OrderBL())
                {
                    List<Order> orderHistory = new List<Order>();

                    if (userType == Helpers.UserType.Retailer)
                    {
                       orderHistory = await orderAccess.GetOrderByRetailerIDBL(currUserID);
                    }
                    if (userType == Helpers.UserType.SalesPerson)
                    {
                        orderHistory = await orderAccess.GetOrderBySalesPersonIDBL(currUserID);
                    }
                    //Print order history
                    Console.WriteLine("ORDERS HISTORY: ");
                    Console.WriteLine("#\tNo\tOrder ID\tTotal Quantity\tTotal Amount\tOrder Date & Time\t");
                    if (orderHistory != null && orderHistory?.Count > 0)
                    {
                        Console.WriteLine(".......................");
                        int serial = 0;
                        foreach (Order order in orderHistory)
                        {
                            serial++;
                            Console.WriteLine($"{serial}\t{order.OrderID}\t{order.TotalQuantity}\t{order.TotalAmount}\t{order.OrderDateTime}");
                        }
                    }
                    Console.WriteLine("Select Order to Initiate Cancellation Process: ");
                    int cancelSelection = int.Parse(Console.ReadLine()) - 1;

                    using (IOrderDetailsBL accessOrderDetails = new OrderDetailsBL())
                    {
                        List<OrderDetail> orderDetail = new List<OrderDetail>();
                        orderDetail = await accessOrderDetails.GetOrderDetailsByOrderIDBL(orderHistory[cancelSelection].OrderID);
                        Console.WriteLine("Products in this Order: ");
                        if (orderDetail != null && orderDetail?.Count > 0)
                        {
                            Console.WriteLine(".......................");
                            int serial = 0;
                            foreach (var product in orderDetail)
                            {
                                serial++;
                                ProductBL productBL = new ProductBL();
                                string productName = (await productBL.GetProductByProductIDBL(product.ProductID)).ProductName;
                                Console.WriteLine($"{serial}\t{productName}\t{product.Quantity}\t{product.DiscountedUnitPrice}");
                            }
                        }
                        Console.WriteLine("Select the Product you want to cancel:");
                        int cancelProductChoice = int.Parse(Console.ReadLine())-1;
                        if (orderDetail[cancelProductChoice].CurrentStatus == "Cancel")
                        {
                            Console.WriteLine("Already Cancelled");
                        }
                        else if (orderDetail[cancelProductChoice].CurrentStatus == "Delivered")
                        {
                            Console.WriteLine("Already Delivered, Cancellation not Possible");
                        }
                        else if (orderDetail[cancelProductChoice].CurrentStatus == "Return")
                        {
                            Console.WriteLine("Product has been Returned");
                        }
                        /*else if (orderDetail[cancelProductChoice].Status == ProductStatus.InCart)
                        {
                            Console.WriteLine("Product is in Cart");
                        }*/
                        else
                        {
                            orderDetail[cancelProductChoice].CurrentStatus = "Cancel";
                            Console.WriteLine("Product Status:Cancelled");
                        }


                    }
                    


                }





            }
            catch (GreatOutdoorsException ex)
            {
                ExceptionLogger.LogException(ex);
                Console.WriteLine(ex.Message);
            }

        }

    }
}
