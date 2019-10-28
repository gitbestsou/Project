using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreatOutdoors.MVC.Models;
using Capgemini.GreatOutdoors.BusinessLayer;
using GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using System.Threading.Tasks;

namespace GreatOutdoors.MVC.Controllers
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
            IEnumerable<Order> orders =await orderBL.GetAllOrdersBL();
            List<OrderViewModel> ordersVM = new List<OrderViewModel>();

            foreach(var item in orders)
            {
                OrderViewModel orderVM = new OrderViewModel() {OrderID = item.OrderID,TotalQuantity = item.TotalQuantity,TotalAmount = item.TotalAmount,ChannelOfSale = item.ChannelOfSale,OrderDateTime = (DateTime)item.OrderDateTime };
                ordersVM.Add(orderVM);
            }
            return View(ordersVM);
           
        }

        public async Task<ActionResult> OrderDetails(Guid Id)
        {
            OrderDetailsBL orderDetailsBL = new OrderDetailsBL();
            IEnumerable<OrderDetail> orderDetails = await orderDetailsBL.GetOrderDetailsByOrderIDBL(Id);
            List<OrderDetailViewModel> orderDetailsVM = new List<OrderDetailViewModel>();
            
            foreach(var item in orderDetails)
            {
                ProductBL productBL = new ProductBL();
                Product product = new Product();
                product =await productBL.GetProductByProductIDBL(item.ProductID);

                OrderDetailViewModel orderDetailVM = new OrderDetailViewModel()
                { OrderDetailID = item.OrderDetailID,OrderID = item.OrderID,ProductName = product.Name,Quantity = item.Quantity,TotalPrice = item.TotalPrice,CurrentStatus = item.CurrentStatus};
                orderDetailsVM.Add(orderDetailVM);
            }
            return View(orderDetailsVM);
        }


    }
}