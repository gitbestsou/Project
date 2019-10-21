using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.BusinessLayer;
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
    public static class PlaceReturn
    {
        public static async Task ReturnRequest(Guid currUserID,Helpers.UserType userType)
        {
            try
            {
                using (IReturnBL returnAccess = new ReturnBL())
                {
                    Return returnObject = new Return();
                    //returnObject.ReturnID = Guid.NewGuid();
                    (bool returnConfirmed, Guid Id) = await returnAccess.AddReturnBL(returnObject);
                    //Display Orders
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
                        Console.WriteLine("--Select an Order to Initiate a Return--: ");
                        Console.WriteLine("#\tOrderID\tQuantity\tTotal Amount\tOrder Date\tAddress ID\tChannel Of Sale");
                        if (orderHistory != null && orderHistory?.Count > 0)
                        {
                            Console.WriteLine(".......................");
                            int serial = 0;
                            foreach (Order order in orderHistory)
                            {
                                serial++;
                                Console.WriteLine($"{serial}\t{order.OrderID}\t{order.TotalQuantity}\t{order.TotalAmount}\t{order.OrderDateTime}\t{order.ChannelOfSale}");
                            }
                        }

                        int returnSelection = int.Parse(Console.ReadLine());
                        using (IOrderDetailsBL accessOrderDetails = new OrderDetailsBL())
                        {
                            List<OrderDetail> orderDetail = new List<OrderDetail>();
                            orderDetail = await accessOrderDetails.GetOrderDetailsByOrderIDBL(orderHistory[returnSelection - 1].OrderID);

                            // Printing products in the order
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
                                    Console.WriteLine($"{serial}\t{productName}\t{product.Quantity}\t{product.CurrentStatus}\t{product.DiscountedUnitPrice}\t{product.TotalPrice}");
                                }
                            }
                            using (IReturnDetailsBL accessReturnDetails = new ReturnDetailsBL())
                            {
                                bool reply = true;
                                decimal totalReturnAmount = 0;
                                int count = 0;
                                while (reply)
                                {
                                    ReturnDetail currentReturn = new ReturnDetail();
                                    currentReturn.ReturnDetailID = Guid.NewGuid();
                                    currentReturn.ReturnID = Id;
                                    Console.WriteLine("Select the product : ");
                                    int choice = int.Parse(Console.ReadLine()) - 1;


                                    //currentReturn.AddressID = orderDetail[int.Parse(Console.ReadLine()) - 1].AddressID;
                                    //currentReturn.Quantity = orderDetail[choice].Quantity;
                                    /*while (true)
                                    {
                                        Console.WriteLine("How many quantities you want to return?");
                                        int quantity = Convert.ToInt32(Console.ReadLine());
                                        if (quantity <= orderDetail[choice].Quantity && quantity > 0)
                                        {
                                            currentReturn.Quantity = quantity;
                                            break;
                                        }
                                        Console.WriteLine("Invalid Return Quantity.");
                                    }*/
                                    if (orderDetail[choice].CurrentStatus == "Cancel")
                                    {
                                        Console.WriteLine("Product has been cancelled");
                                    }
                                    else if (orderDetail[choice].CurrentStatus == "Return")
                                    {
                                        Console.WriteLine("Product has already been returned");
                                    }
                                    /*else if (orderDetail[choice].Status == ProductStatus.InCart)
                                    {
                                        Console.WriteLine("Product is in your cart");
                                    }
                                    else if (orderDetail[choice].Status == ProductStatus.Shipped)
                                    {
                                        Console.WriteLine("Product has been shipped but not delivered,you can cancel");
                                    }
                                    else if (orderDetail[choice].Status == ProductStatus.UnderProcessing)
                                    {
                                        Console.WriteLine("Product is under processing, it hasn't been shipped yet");
                                    }*/
                                    else
                                    {
                                        currentReturn.ProductID = orderDetail[choice].ProductID;
                                        currentReturn.Quantity = orderDetail[choice].Quantity;
                                        Console.WriteLine(orderDetail[choice].DiscountedUnitPrice);
                                        currentReturn.UnitPrice = orderDetail[choice].DiscountedUnitPrice * currentReturn.Quantity;
                                        currentReturn.TotalPrice = orderDetail[choice].TotalPrice;
                                        //currentReturn.TotalPrice = orderDetail[choice].TotalPrice;
                                        totalReturnAmount = currentReturn.TotalPrice;
                                        Console.WriteLine("Reason Of Return : " + "\n" + "1. Wrong Order Shipped" + "\n" + "2. Incomplete Order Shipped" + "\n" + "Enter the Choice(1/2) :  ");
                                        int reasonEnum = int.Parse(Console.ReadLine());
                                        if (reasonEnum == 1)
                                            currentReturn.ReasonOfReturn = "Wrong";
                                        if (reasonEnum == 2)
                                            currentReturn.ReasonOfReturn = "Incomplete";
                                        bool isReturnAdded = await accessReturnDetails.AddReturnDetailsBL(currentReturn);
                                        count++;
                                        


                                        Console.WriteLine("You want to return products worth " + totalReturnAmount + ". Confirm Return(Y/N) ?");
                                        string makeReturn = Console.ReadLine().ToLower();

                                        if (makeReturn == "y")
                                        {
                                            orderDetail[choice].CurrentStatus = "Return";
                                            if (userType == Helpers.UserType.Retailer)
                                            {
                                                returnObject.ChannelOfReturn = "Online";
                                            }
                                            if (userType == Helpers.UserType.SalesPerson)
                                            {
                                                returnObject.ChannelOfReturn = "Offline";
                                            }
                                            returnObject.OrderID = orderHistory[returnSelection - 1].OrderID;
                                            returnObject.ReturnAmount = totalReturnAmount;
                                            returnObject.ReturnDateTime = DateTime.Now;
                                            Console.WriteLine("Return Confirmed.");
                                        }
                                        else
                                        {
                                            // Delete order from list
                                            List<ReturnDetail> delete = await accessReturnDetails.GetReturnDetailsByReturnIDBL(Id);
                                            int length = delete.Count();
                                            while (count > 0)
                                            {
                                                length--;
                                                await accessReturnDetails.DeleteReturnDetailsBL((Guid)delete[count].ReturnID, (Guid)delete[count].ProductID);
                                                count--;

                                            }
                                            Console.WriteLine("Thank you for shopping with us.");
                                            await returnAccess.DeleteReturnBL(Id);

                                        }
                                        Console.WriteLine("You want to return another product(Y/N) ? : ");
                                        string replyConsole = Console.ReadLine().ToLower();
                                        if (replyConsole.ToLower().Equals("y"))
                                            reply = true;
                                        else
                                            reply = false;
                                        if (count == orderDetail.Count)
                                        {
                                            Console.WriteLine("There is no more product left to return");
                                            reply = false;
                                        }
                                    }
                                }

                            }



                        }
                    }
                }
                Console.WriteLine();
            }
            catch (GreatOutdoorsException ex)
            {
                ExceptionLogger.LogException(ex);
                Console.WriteLine(ex.Message);
            }
        }





        public static async Task ViewReturns(Guid currUserID,Helpers.UserType userType)
        {

            try
            {
                using (IReturnBL returnAccess = new ReturnBL())
                {
                    IOrdersBL orderAccess = new OrderBL();
                    List<Order> orderHistory = new List<Order>();
                    if (userType == Helpers.UserType.Retailer)
                    { 
                        orderHistory = await orderAccess.GetOrderByRetailerIDBL(currUserID);
                    }
                    if (userType == Helpers.UserType.SalesPerson)
                    {
                        orderHistory = await orderAccess.GetOrderBySalesPersonIDBL(currUserID);
                    }
                    List<Return> totalReturnList = await returnAccess.GetAllReturnsBL();
                    List<Return> returnHistory = new List<Return>();
                    foreach(var orderObject in orderHistory)
                    {
                        foreach(var returnObject in totalReturnList)
                        {
                            if (returnObject.OrderID == orderObject.OrderID)
                                returnHistory.Add(returnObject);
                        }
                    }


                    //List<Return> returnHistory = await returnAccess.GetReturnByRetailerIDBL(currUserID);
                    //Print Return history
                    Console.WriteLine("RETURNS HISTORY: ");
                    Console.WriteLine("#\tReturnID\tChannel Of Return\t Return Amount\tReturn Date and Time");
                    if (returnHistory != null && returnHistory?.Count > 0)
                    {
                        Console.WriteLine(".......................");
                        int serial = 0;
                        foreach (Return returnObj in returnHistory)
                        {
                            serial++;
                            Console.WriteLine($"{serial}\t{returnObj.ReturnID}\t{returnObj.ChannelOfReturn}\t{returnObj.ReturnAmount}\t{returnObj.ReturnDateTime}");

                        }
                    }

                    Console.WriteLine("Please Select the Return  : ");
                    int returnSelection = int.Parse(Console.ReadLine()) - 1;

                    using (IReturnDetailsBL accessReturnDetails = new ReturnDetailsBL())
                    {
                        List<ReturnDetail> returnDetail = new List<ReturnDetail>();
                        returnDetail = await accessReturnDetails.GetReturnDetailsByReturnIDBL(returnHistory[returnSelection].ReturnID);
                        Console.WriteLine(returnDetail?.Count);
                        // Printing products in the order
                        Console.WriteLine("Products in this Return: ");
                        if (returnDetail != null && returnDetail?.Count > 0)
                        {
                            Console.WriteLine(".......................");
                            int serial = 0;
                            foreach (var product in returnDetail)
                            {

                                ProductBL productBL = new ProductBL();
                                string productName = (await productBL.GetProductByProductIDBL((Guid)product.ProductID)).ProductName;
                                Console.WriteLine($"{serial + 1}\t{productName}\t{returnDetail[serial].Quantity}\t{returnDetail[serial].ReasonOfReturn}\t{returnDetail[serial].TotalPrice}\t");
                                serial++;
                            }
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