using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreatOutdoors.Mvc.Models;
using Capgemini.GreatOutdoors.BusinessLayer;
using GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using System.Threading.Tasks;
using System.Net.Http;

/*Return Controller which contains methods 
 *  1. View Order History. 2. View Corresponding Order Details, 3. Perform Return, 4. Perform Cancel, 5. View Return History
Project name : Great Outdoors
Developer name: Sourav Maji
Use case : Return
Creation date : 30/10/2019
Last modified : 05/11/2019
 */

namespace GreatOutdoors.MVC.Controllers
{
    public class ReturnsController : Controller
    {
        public async Task<ActionResult> ReturnHistory()
        {
            Guid id = (Guid)Session["RetailerID"]; // Get the ID of the Retailer who has logged in
            List<ReturnViewModel> returnsVM = new List<ReturnViewModel>();



            //WebAPI code has been commented

            //Using WebAPI to view Return History
            //using (var client = new HttpClient())
            //{
                   //client.BaseAddress = new Uri("http://localhost:50075/api/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync($"ReturnsAPI?id={id}");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<List<ReturnViewModel>>();
            //        readTask.Wait();

            //        returnsVM = readTask.Result;
            //    }

            //}







            /*Commenting the WCF Service code. This code needs to be uncommented if you want to use WCF Service*/
            //ReturnDetailsBL returnDetailsBL = new ReturnDetailsBL();
            //List<ReturnDetail> returnDetails = new List<ReturnDetail>();
            //Guid IDD = (Guid)Session["RetailerID"]; // Retrieving the Retailer ID who has logged in

            ////Calling WCF Service
            //ServiceReference1.Service1Client returnsServiceClient = new ServiceReference1.Service1Client();
            //WCFService.ReturnDetailsDataContract[] returnsDC = returnsServiceClient.GetReturnDetailsByRetailerIDDAL(IDD);

            //List<ReturnViewModel> returnsVM = new List<ReturnViewModel>();
            //foreach (var item in returnsDC)
            //{
            //    ReturnViewModel newreturnVm = new ReturnViewModel();
            //    newreturnVm.ReturnID = (Guid)item.ReturnID;
            //    newreturnVm.ReturnDetailID = item.ReturnDetailID;
            //    newreturnVm.ProductID = (Guid)item.ProductID;
            //    newreturnVm.ReasonOfReturn = item.ReasonOfReturn;
            //    newreturnVm.Quantity = item.Quantity;
            //    newreturnVm.UnitPrice = item.UnitPrice;
            //    newreturnVm.TotalAmount = item.TotalPrice;

            //    ProductBL productBL = new ProductBL();
            //    Product product = new Product();
            //    product = await productBL.GetProductByProductIDBL((Guid)item.ProductID);
            //    newreturnVm.ProductName = product.Name;

            //    ReturnBL returnBL = new ReturnBL();
            //    Return @return = new Return();
            //    @return = await returnBL.GetReturnByReturnIDBL((Guid)item.ReturnID);
            //    newreturnVm.ReturnDateTime = @return.ReturnDateTime;
            //    returnsVM.Add(newreturnVm);
            //}


            List<ReturnDetail> returnDetails = new List<ReturnDetail>();
            ReturnDetailsBL returnDetailsBL = new ReturnDetailsBL();
            returnDetails = await returnDetailsBL.GetReturnDetailsByRetailerIDBL(id);

            List<ReturnViewModel> returnsVM1 = new List<ReturnViewModel>();

            foreach(var item in returnDetails)
            {
                //Declaring Product and Return to view the Product Name and ReturnDateTime
                ProductBL productBL = new ProductBL();
                Product product = new Product();
                product = await productBL.GetProductByProductIDBL((Guid)item.ProductID);

                ReturnBL returnBL = new ReturnBL();
                Return @return = new Return();
                @return = await returnBL.GetReturnByReturnIDBL((Guid)item.ReturnID);
                ReturnViewModel returnViewModel = new ReturnViewModel()
                {
                    ReturnID = (Guid)item.ReturnID,
                    ReturnDetailID = item.ReturnDetailID,
                    ProductID = (Guid)item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Quantity,
                    ReasonOfReturn = item.ReasonOfReturn,
                    TotalAmount = item.TotalPrice,
                    ProductName = product.Name,
                    ReturnDateTime = @return.ReturnDateTime
                };
                returnsVM1.Add(returnViewModel);
                
            }







            return View(returnsVM1);
        }











        //View Order History
        //GET:OrderHistory
        //URL:Retuns/OrderHistory
        public ActionResult OrderHistory()
        {
            OrderBL orderBL = new OrderBL();
            Guid id = (Guid)Session["RetailerID"]; // Get the ID of the Retailer who has logged in
            List<OrderViewModel> ordersVM = new List<OrderViewModel>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55089/api/");
                //HTTP GET
                var responseTask = client.GetAsync($"ReturnsAPI?id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<OrderViewModel>>();
                    readTask.Wait();

                    ordersVM = readTask.Result;
                }

            }



            //List<Order> orders = await orderBL.GetOrderByRetailerIDBL(id); //Retrieving all the orders            
            //foreach (var item in orders)
            //{
            //    OrderViewModel orderVM = new OrderViewModel() { OrderID = item.OrderID, TotalQuantity = item.TotalQuantity, TotalAmount = item.TotalAmount, ChannelOfSale = item.ChannelOfSale, OrderDateTime = (DateTime)item.OrderDateTime }; // Setting the viewmodel properties to the properties of order
            //    ordersVM.Add(orderVM);
            //}


            return View(ordersVM); // adding each individual view to the entire ViewModel








        }

        //View Sales History
        //GET:SalesHistory
        //URL:Retuns/SalesHistory

        public async Task<ActionResult> SalesHistory()
        {
            OrderBL orderBL = new OrderBL();

            Guid id = (Guid)Session["SalesPersonID"]; // Get the ID of the Salesprson who has logged in

            List<Order> orders = await orderBL.GetOrderBySalesPersonIDBL(id); //Retrieving all the orders by Salesperson

            List<OrderViewModel> ordersVM = new List<OrderViewModel>();

            foreach (var item in orders)
            {
                RetailerBL retailerBL = new RetailerBL();

                Retailer retailer = await retailerBL.GetRetailerByRetailerIDBL((Guid)item.RetailerID);
                OrderViewModel orderVM = new OrderViewModel() { OrderID = item.OrderID,RetailerName= retailer.RetailerName, TotalQuantity = item.TotalQuantity, TotalAmount = item.TotalAmount, ChannelOfSale = item.ChannelOfSale, OrderDateTime = (DateTime)item.OrderDateTime };  // Setting the viewmodel properties to the properties of order

                ordersVM.Add(orderVM);
            }

            return View(ordersVM);

        }

        //View Order Details of a corresponding Order
        //GET:OrderDetails
        //URL:Retuns/OrderDetails

        public async Task<ActionResult> OrderDetails(Guid Id)
        {
           
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            List<OrderDetail> orderDetails = await orderDetailsBL.GetOrderDetailsByOrderIDBL(Id);
            List<OrderDetailViewModel> orderDetailsVM = new List<OrderDetailViewModel>();           
            foreach (var item in orderDetails)
            {
                ProductBL productBL = new ProductBL(); 
                Product product = new Product();//Initiating a product object to show the Product Details simultaneously 
                product = await productBL.GetProductByProductIDBL(item.ProductID); // Retrieving  the product object details 
                OrderDetailViewModel orderDetailVM = new OrderDetailViewModel()
                { OrderDetailID = item.OrderDetailID, OrderID = item.OrderID, ProductName = product.Name, Quantity = item.Quantity, TotalPrice = item.TotalPrice, CurrentStatus = item.CurrentStatus };                
                orderDetailsVM.Add(orderDetailVM);
               
            }
            return View(orderDetailsVM);
        }

        //Place a Return
        //GET:PlaceReturn
        //URL:Retuns/PlaceReturn
        public async Task<ActionResult> PlaceReturn(Guid Id)
        {
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            ProductBL productBL = new ProductBL();
            Product product = new Product();//Initiating a product object to show the Product Details simultaneously 
            product = await productBL.GetProductByProductIDBL(orderDetail.ProductID); // Retrieving  the product object details 

            AddressBL addressBL = new AddressBL();
            Address address = new Address(); //Creating an address object to show the Address Details in the Return page
            address = await addressBL.GetAddressByAddressIDBL(orderDetail.AddressID); //Retrieving the address details

            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                ProductName = product.Name,
                Quantity = orderDetail.Quantity,
                TotalPrice = orderDetail.TotalPrice,
                CurrentStatus = orderDetail.CurrentStatus,
                City = address.City,
                State = address.State,
                PinCode = address.Pincode,
                MobileNo = address.MobileNo
            };          
            return View(orderDetailViewModel);            
        }

        //Store the relevant Data after placing a Return and redirect to OrderDetails
        //POST: Placing a Return 
        //URL:Retuns/PlaceReturn

        [HttpPost]
        public async Task<ActionResult> PlaceReturn(OrderDetailViewModel orderDetailViewModel)
        {
            Guid Id = orderDetailViewModel.OrderDetailID;
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            /*A product cannot be returned or cancelled if it is cancelled or returned. Since I disabled the Return and cancel button when the statsus is 'Returned' or 'Cancelled', therefore,below code is commented.*/

            //if (orderDetail.CurrentStatus=="Returned" || orderDetail.CurrentStatus=="Cancelled")
            //{
            //    ViewBag.Message = string.Format("Return not possible");
            //    return View(orderDetailViewModel);

            //}

            if (orderDetail.CurrentStatus != "Returned" && orderDetail.CurrentStatus != "Cancelled")
            {
                orderDetailsBL.UpdateOrderDetailForReturn(orderDetail.OrderDetailID, "Returned"); //Changing the Order Status
                OrderBL orderBL = new OrderBL();
                Order order = new Order();
                Return returnObj = new Return(); //Creating a return object to add return information
                returnObj.ReturnID = Guid.NewGuid();
                returnObj.OrderID = orderDetail.OrderID;
                returnObj.ReturnDateTime = DateTime.Now;
                returnObj.LastModifiedDateTime = DateTime.Now;
                ReturnDetail returnDetail = new ReturnDetail();//Creating a returndetail object to add returndetail information
                returnDetail.ReturnDetailID = Guid.NewGuid();
                returnDetail.ReturnID = returnObj.ReturnID;
                returnDetail.ProductID = orderDetail.ProductID;
                returnDetail.UnitPrice = orderDetail.DiscountedUnitPrice;
                returnDetail.TotalPrice = orderDetail.TotalPrice;
                returnDetail.Quantity = orderDetail.Quantity;
                returnDetail.AddressID = orderDetail.AddressID;
                returnDetail.ReasonOfReturn = orderDetailViewModel.ReasonOfReturn;
                
                using (IReturnBL returnBL = new ReturnBL())
                {
                    order = await orderBL.GetOrderByOrderIDBL(orderDetail.OrderID); //Initializing the order object to retrieve the information about chanel and total amount
                    returnObj.ChannelOfReturn = order.ChannelOfSale;
                    returnObj.ReturnAmount = order.TotalAmount; 
                    (bool isAdded, Guid Id_here) = await returnBL.AddReturnBL(returnObj);
                    using (IReturnDetailsBL returnDetailsBL = new ReturnDetailsBL())
                    {
                        bool isAdded1 = await returnDetailsBL.AddReturnDetailsBL(returnDetail);

                    }
                }
            }


            return RedirectToAction("OrderDetails",new { id= orderDetail.OrderID});
        }

        //Place a Cancellation of Product
        //GET:RequestCancel
        //URL:Retuns/RequestCancel
        public async Task<ActionResult> RequestCancel(Guid Id)
        {
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            ProductBL productBL = new ProductBL();
            Product product = new Product(); //Initializing the product object to show the product Information
            product = await productBL.GetProductByProductIDBL(orderDetail.ProductID); // Retrieve product information

            AddressBL addressBL = new AddressBL();
            Address address = new Address(); // Initializing the addrss object to show the address information 
            address = await addressBL.GetAddressByAddressIDBL(orderDetail.AddressID); //Retrieving the address information

            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                ProductName = product.Name,
                Quantity = orderDetail.Quantity,
                TotalPrice = orderDetail.TotalPrice,
                CurrentStatus = orderDetail.CurrentStatus,
                City = address.City,
                State = address.State,
                PinCode = address.Pincode,
                MobileNo = address.MobileNo
            };

            return View(orderDetailViewModel);
        }


        //View Order History
        //GET:OrderHistory
        //URL:Retuns/OrderHistory
        [HttpPost]
        public async Task<ActionResult> RequestCancel(OrderDetailViewModel orderDetailViewModel)
        {
            Guid Id = orderDetailViewModel.OrderDetailID;
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            /*A product cannot be returned or cancelled if it is cancelled or returned. Since I disabled the Return and cancel button when the statsus is 'Returned' or 'Cancelled', therefore,below code is commented.*/

            //if (orderDetail.CurrentStatus == "Returned" || orderDetail.CurrentStatus == "Cancelled")
            //{
            //    ViewBag.Message = string.Format("Cancel not possible");
            //    return View(orderDetailViewModel);

            //}
            if (orderDetail.CurrentStatus != "Returned" && orderDetail.CurrentStatus != "Cancelled")
            {
                orderDetailsBL.UpdateOrderDetailForReturn(orderDetail.OrderDetailID, "Cancelled");
            }
            return RedirectToAction("OrderDetails", new { id = orderDetail.OrderID });
        }


        //View Return History
        //GET:ReturnHistory
        //URL:Retuns/ReturnHistory

        
    }
}
