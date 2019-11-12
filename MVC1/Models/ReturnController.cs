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

namespace GreatOutdoors.Mvc.Controllers
{
    public class ReturnsController : Controller
    {
        //Creating and initializing viewmodel object
        // GET: Returns
        //URL:Returns/Create
        public async Task<ActionResult> Create()
        {
            Order order = new Order();
            List<Order> orders = new List<Order>();
            using (IOrdersBL ordersBL = new OrderBL())
            {
                orders = await ordersBL.GetAllOrdersBL();
            }
            ViewBag.orderlist = new SelectList(orders, "OrderID", "OrderDateTime");

            ReturnViewModel returnViewModel = new ReturnViewModel();
            {
                //ReasonOfReturn = "Wrong"
            };
            return View(returnViewModel);
        }

        public async Task<ActionResult> OrderHistory()
        {
            OrderBL orderBL = new OrderBL();

            List<Order> orders = await orderBL.GetAllOrdersBL();

            List<OrderViewModel> ordersVM = new List<OrderViewModel>();

            foreach (var item in orders)
            {
                OrderViewModel orderVM = new OrderViewModel() { OrderID = item.OrderID, TotalQuantity = item.TotalQuantity, TotalAmount = item.TotalAmount, ChannelOfSale = item.ChannelOfSale, OrderDateTime = (DateTime)item.OrderDateTime };
                ordersVM.Add(orderVM);
            }
            return View(ordersVM);

        }

        public async Task<ActionResult> OrderDetails(Guid Id)
        {
           
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            List<OrderDetail> orderDetails = await orderDetailsBL.GetOrderDetailsByOrderIDBL(Id);
            List<OrderDetailViewModel> orderDetailsVM = new List<OrderDetailViewModel>();

           
            foreach (var item in orderDetails)
            {
                ProductBL productBL = new ProductBL();
                Product product = new Product();
                product = await productBL.GetProductByProductIDBL(item.ProductID);

                OrderDetailViewModel orderDetailVM = new OrderDetailViewModel()
                { OrderDetailID = item.OrderDetailID, OrderID = item.OrderID, ProductName = product.Name, Quantity = item.Quantity, TotalPrice = item.TotalPrice, CurrentStatus = item.CurrentStatus };                
                orderDetailsVM.Add(orderDetailVM);
               
            }
            return View(orderDetailsVM);
        }

        public async Task<ActionResult> PlaceReturn(Guid Id)
        {
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            ProductBL productBL = new ProductBL();
            Product product = new Product();
            product = await productBL.GetProductByProductIDBL(orderDetail.ProductID);

            AddressBL addressBL = new AddressBL();
            Address address = new Address();
            address = await addressBL.GetAddressByAddressIDBL(orderDetail.AddressID);

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

        [HttpPost]
        public async Task<ActionResult> PlaceReturn(OrderDetailViewModel orderDetailViewModel)
        {
            Guid Id = orderDetailViewModel.OrderDetailID;
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            if(orderDetail.CurrentStatus=="Returned" || orderDetail.CurrentStatus=="Cancelled")
            {
                ViewBag.Message = string.Format("Return not possible");
                return View(orderDetailViewModel);

            }
            if (orderDetail.CurrentStatus != "Returned" && orderDetail.CurrentStatus != "Cancelled")
            {
                orderDetailsBL.UpdateOrderDetailForReturn(orderDetail.OrderDetailID, "Returned");

                OrderBL orderBL = new OrderBL();
                Order order = new Order();


                Return returnObj = new Return();
                returnObj.ReturnID = Guid.NewGuid();
                returnObj.OrderID = orderDetail.OrderID;
                returnObj.ReturnDateTime = DateTime.Now;
                returnObj.LastModifiedDateTime = DateTime.Now;

                ReturnDetail returnDetail = new ReturnDetail();
                returnDetail.ReturnDetailID = Guid.NewGuid();
                returnDetail.ReturnID = returnObj.ReturnID;
                returnDetail.ProductID = orderDetail.ProductID;
                returnDetail.UnitPrice = orderDetail.DiscountedUnitPrice;
                returnDetail.TotalPrice = orderDetail.TotalPrice;
                returnDetail.Quantity = orderDetail.Quantity;
                returnDetail.AddressID = orderDetail.AddressID;

                returnDetail.ReasonOfReturn = orderDetailViewModel.ReasonOfReturn;

                //returnDetail.ReasonOfReturn = "Wrong";


                using (IReturnBL returnBL = new ReturnBL())
                {
                    order = await orderBL.GetOrderByOrderIDBL(orderDetail.OrderID);
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

        public async Task<ActionResult> RequestCancel(Guid Id)
        {
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);

            ProductBL productBL = new ProductBL();
            Product product = new Product();
            product = await productBL.GetProductByProductIDBL(orderDetail.ProductID);

            AddressBL addressBL = new AddressBL();
            Address address = new Address();
            address = await addressBL.GetAddressByAddressIDBL(orderDetail.AddressID);

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

        [HttpPost]
        public async Task<ActionResult> RequestCancel(OrderDetailViewModel orderDetailViewModel)
        {
            Guid Id = orderDetailViewModel.OrderDetailID;
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            OrderDetail orderDetail = await orderDetailsBL.GetOrderDetailByOrderDetailIDBL(Id);
            if (orderDetail.CurrentStatus == "Returned" || orderDetail.CurrentStatus == "Cancelled")
            {
                ViewBag.Message = string.Format("Cancel not possible");
                return View(orderDetailViewModel);

            }
            if (orderDetail.CurrentStatus != "Returned" && orderDetail.CurrentStatus != "Cancelled")
            {
                orderDetailsBL.UpdateOrderDetailForReturn(orderDetail.OrderDetailID, "Cancelled");
            }
            return RedirectToAction("OrderDetails", new { id = orderDetail.OrderID });
        }

        public async Task<ActionResult> ReturnHistory()
        {
            ReturnDetailsBL returnDetailsBL = new ReturnDetailsBL();
            List<ReturnDetail> returnDetails = new List<ReturnDetail>();
            returnDetails = await returnDetailsBL.GetAllReturnDetailsBL();
            List<ReturnViewModel> returnsVM = new List<ReturnViewModel>();

            foreach (var item in returnDetails)
            {
                ProductBL productBL = new ProductBL();
                Product product = new Product();
                product = await productBL.GetProductByProductIDBL((Guid)item.ProductID);

                ReturnBL returnBL = new ReturnBL();
                Return @return = new Return();
                @return =await returnBL.GetReturnByReturnIDBL((Guid)item.ReturnID);

                ReturnViewModel returnVM = new ReturnViewModel() {ProductName=product.Name,Quantity = item.Quantity,TotalAmount=item.TotalPrice,ReasonOfReturn = item.ReasonOfReturn,ReturnDateTime= @return.ReturnDateTime};
                returnsVM.Add(returnVM);
            }
            return View(returnsVM);
        }

    }
}