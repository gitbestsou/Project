using Capgemini.GreatOutdoors.BusinessLayer;
using GreatOutdoors.Entities;
using GreatOutdoors.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GreatOutdoors.API.Controllers
{
    /*Return API Controller
    Project name : Great Outdoors
    Developer name: Sourav Maji
    Use case : Return
    Creation date : 20/11/2019
    Last modified : 20/11/2019
    */
    public class ReturnsAPIController : ApiController
    {
        //Declaring the ReturnDetails Business Layer 
        private ReturnDetailsBL returnAccessBL;
        private OrderBL orderAccessBL;

        public ReturnsAPIController()
        {
            //Declaring the Return Business Layer 
            returnAccessBL = new ReturnDetailsBL();
            orderAccessBL = new OrderBL();
        }

        public async Task<IHttpActionResult> GetOrderByRetailerIDDAL(Guid id)
        {
            List<Order> orders = await orderAccessBL.GetOrderByRetailerIDBL(id);
            List<OrderViewModel> ordersVM = new List<OrderViewModel>();
            foreach (var item in orders)
            {
                OrderViewModel orderVM = new OrderViewModel() { OrderID = item.OrderID,RetailerID=(Guid)item.RetailerID, TotalQuantity = item.TotalQuantity, TotalAmount = item.TotalAmount, ChannelOfSale = item.ChannelOfSale, OrderDateTime = (DateTime)item.OrderDateTime }; // Setting the viewmodel properties to the properties of order
                ordersVM.Add(orderVM);
            }

            if(orders.Count==0)
            {
                return NotFound();
            }

            return Ok(ordersVM);
        }




        //public async Task<IHttpActionResult> GetReturnDetailsByRetailerIDDAL(Guid id)
        //{
        //    // List of the returnDetails from Business layer
            
        //    List<ReturnDetail> returnDetails = await returnAccessBL.GetReturnDetailsByRetailerIDBL(id);
        //    //Declaring the return viewModel list
        //    List<ReturnViewModel> returnsVM = new List<ReturnViewModel>();


        //    foreach(var item in returnDetails)
        //    {
        //        //Declaring Product and Return to view the Product Name and ReturnDateTime
        //        ProductBL productBL = new ProductBL();
        //        Product product = new Product();
        //        product = await productBL.GetProductByProductIDBL((Guid)item.ProductID);

        //        ReturnBL returnBL = new ReturnBL();
        //        Return @return = new Return();
        //        @return = await returnBL.GetReturnByReturnIDBL((Guid)item.ReturnID);
        //        ReturnViewModel returnViewModel = new ReturnViewModel()
        //        {
        //            ReturnID = (Guid)item.ReturnID,
        //            ReturnDetailID = item.ReturnDetailID,
        //            ProductID = (Guid)item.ProductID,
        //            Quantity = item.Quantity,
        //            UnitPrice = item.Quantity,
        //            ReasonOfReturn = item.ReasonOfReturn,
        //            TotalAmount = item.TotalPrice,
        //            ProductName = product.Name,
        //            ReturnDateTime = @return.ReturnDateTime
        //        };
                


        //        returnsVM.Add(returnViewModel);

        //    }

             

        //    if (returnDetails.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(returnsVM);

        //}




        
    }
}