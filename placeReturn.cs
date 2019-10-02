using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.BusinessLayer;
using Capgemini.GreatOutdoors.DataAccessLayer;
using Capgemini.GreatOutdoors.Entities;
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
        public static async Task ReturnRequest(Guid currRetailerID)
        {
            try
            {
                using (IReturnBL returnAccess = new ReturnBL())
                {
                    Return returnObject = new Return();
                    (bool returnConfirmed, Guid Id) = await returnAccess.AddReturnBL(returnObject);
                    //Display Orders
                    using (IOrdersBL orderAccess = new OrderBL())
                    {
                        List<Order> orderHistory = await orderAccess.GetOrderByRetailerIDBL(currRetailerID);
                        //Print order history
                        Console.WriteLine("--Select an Order to Initiate a Return--: ");
                        Console.WriteLine("#\tOrderID\tQuantity\tTotal Amount\tOrder Date\tCurrent Status\tShipping Address");
                        if (orderHistory != null && orderHistory?.Count > 0)
                        {
                            Console.WriteLine(".......................");
                            int serial = 0;
                            foreach (Order order in orderHistory)
                            {
                                serial++;
                                Console.WriteLine($"{serial}\t{order.OrderID}\t{order.TotalQuantity}\t{order.TotalAmount}\t{order.OrderDateTime}\t{order.CurrentStatus}\t{order.ShippingAddress}");

                            }
                        }

                        int returnSelection = int.Parse(Console.ReadLine());
                        //int orderSelection = int.Parse(Console.ReadLine());
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
                                    Console.WriteLine($"{serial}\t{productName}\t{product.Quantity}\t{product.UnitPrice}");
                                }
                            }
                            using (IReturnDetailsBL accessReturnDetails = new ReturnDetailsBL())
                            {
                                bool reply = true;
                                double totalReturnAmount = 0;
                                int count = 0;
                                while (reply)
                                {
                                    ReturnDetails currentReturn = new ReturnDetails();
                                    currentReturn.ReturnDetailID = Guid.NewGuid();
                                    currentReturn.ReturnID = Id;
                                    Console.WriteLine("Select the product : ");
                                    int choice = int.Parse(Console.ReadLine()) - 1;
                                    currentReturn.ProductID = orderDetail[choice].ProductID;
                                    //currentReturn.AddressID = orderDetail[int.Parse(Console.ReadLine()) - 1].AddressID;
                                    //currentReturn.Quantity = orderDetail[choice].Quantity;
                                    
                                    while (true)
                                    {
                                        Console.WriteLine("How many quantities you want to return?");
                                        int quantity = Convert.ToInt32(Console.ReadLine());
                                        if (quantity <= orderDetail[choice].Quantity && quantity>0)
                                        {
                                            currentReturn.Quantity = quantity;
                                            break;
                                        }
                                        Console.WriteLine("Invalid Return Quantity.");
                                    }

                                    currentReturn.UnitPrice = orderDetail[choice].UnitPrice*currentReturn.Quantity;
                                    //currentReturn.TotalPrice = orderDetail[choice].TotalPrice;
                                    totalReturnAmount = totalReturnAmount + currentReturn.UnitPrice;
                                    Console.WriteLine("Reason Of Return : " + "\n" + "1. Wrong Order Shipped" + "\n" + "2. Incomplete Order Shipped" + "\n" + "Enter the Choice(1/2) :  ");
                                    int reasonEnum = int.Parse(Console.ReadLine());
                                    if (reasonEnum == 1)
                                        currentReturn.ReasonOfReturn = ReturnReasons.Wrong;
                                    if (reasonEnum == 2)
                                        currentReturn.ReasonOfReturn = ReturnReasons.Incomplete;
                                    await accessReturnDetails.AddReturnDetailsBL(currentReturn);
                                    count++;
                                    Console.WriteLine("You want to return another product(Y/N) ? : ");
                                    string replyConsole = Console.ReadLine().ToLower();
                                    if (replyConsole.ToLower().Equals("y"))
                                        reply = true;
                                    else
                                        reply = false;
                                }

                                Console.WriteLine("You want to return products worth " + totalReturnAmount + ". Confirm Return(Y/N) ?");
                                string makeReturn = Console.ReadLine().ToLower();

                                if (makeReturn == "y")
                                {

                                    returnObject.ChannelOfReturn = ReturnChannel.Online;
                                    returnObject.OrderID = orderHistory[returnSelection - 1].OrderID;
                                    returnObject.ReturnAmount = totalReturnAmount;
                                    Console.WriteLine("Return Confirmed.");
                                }
                                else
                                {
                                    // Delete order from list
                                    List<ReturnDetails> delete = await accessReturnDetails.GetReturnDetailsByReturnIDBL(Id);
                                    int length = delete.Count();
                                    while (count > 0)
                                    {
                                        length--;
                                        await accessReturnDetails.DeleteReturnDetailsBL(delete[count].ReturnID, delete[count].ProductID);
                                        count--;

                                    }
                                    Console.WriteLine("Thank you for shopping with us.");
                                    await returnAccess.DeleteReturnBL(Id);

                                }


                            }

                            //using (IReturnBL returnBL = new ReturnBL())
                            //{

                            //}



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

    }



}
