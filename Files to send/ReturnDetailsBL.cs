using GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.DataAccessLayer;

namespace Capgemini.GreatOutdoors.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OrderDetailss from OrderDetailss collection.
    /// </summary>
    public class ReturnDetailsBL : BLBase<ReturnDetail>, IReturnDetailsBL, IDisposable
    {
        //fields
        ReturnDetailDALBase ReturnDetailsDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReturnDetailsBL()
        {
            this.ReturnDetailsDAL = new ReturnDetailDAL();
        }



        /// <summary>
        /// Adds new OrderDetails to OrderDetailss collection.
        /// </summary>
        /// <param name="newReturnDetails">Contains the OrderDetails details to be added.</param>
        /// <returns>Determinates whether the new OrderDetails is added.</returns>
        public async Task<bool> AddReturnDetailsBL(ReturnDetail newReturnDetails)
        {
            bool ReturnDetailsAdded = false;
            try
            {
                if (await Validate(newReturnDetails))
                {
                    await Task.Run(() =>
                    {
                        this.ReturnDetailsDAL.AddReturnDetailDAL(newReturnDetails);
                        ReturnDetailsAdded = true;
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReturnDetailsAdded;
        }



        /// <summary>
        /// Gets ReturnDetails based on ReturnID.
        /// </summary>
        /// <param name="searchReturnID">Represents ReturnID to search.</param>
        /// <returns>Returns ReturnDetail object.</returns>
        public async Task<List<ReturnDetail>> GetReturnDetailsByReturnIDBL(Guid searchReturnID)
        {
            List<ReturnDetail> matchingReturnDetails = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingReturnDetails = ReturnDetailsDAL.GetReturnDetailByReturnIDDAL(searchReturnID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        }





        public async Task<List<ReturnDetail>> GetAllReturnDetailsBL()
        {
            List<ReturnDetail> matchingReturnDetails = new List<ReturnDetail>();

            try
            {
                await Task.Run(() =>
                {
                    matchingReturnDetails = ReturnDetailsDAL.GetAllReturnDetailsDAL();
                });
            }

            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        
        }

        /// <summary>
        /// Gets OrderDetails based on OrderDetailsName.
        /// </summary>
        /// <param name="searchProductID">Represents OrderDetailsName to search.</param>
        /// <returns>Returns OrderDetails object.</returns>
        public async Task<List<ReturnDetail>> GetReturnDetailsByProductIDBL(Guid searchProductID)
        {
            List<ReturnDetail> matchingReturnDetails = new List<ReturnDetail>();
            try
            {
                await Task.Run(() =>
                {
                    matchingReturnDetails = ReturnDetailsDAL.GetReturnDetailByProductIDDAL(searchProductID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        }




        public async Task<List<ReturnDetail>> GetReturnDetailsByRetailerIDBL(Guid searchRetailerID)
        {
            List<ReturnDetail> matchingReturnDetails = new List<ReturnDetail>();
            try
            {
                await Task.Run(() =>
                {
                    matchingReturnDetails = ReturnDetailsDAL.GetReturnDetailsByRetailerIDDAL(searchRetailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        }


        /// <summary>
        /// Deletes OrderDetails based on OrderDetailsID.
        /// </summary>
        /// <param name="deleteReturnID">Represents OrderDetailsID to delete.</param>
        /// /// <param name="deleteProducID">Represents OrderDetailsID to delete.</param>
        /// <returns>Determinates whether the existing OrderDetails is updated.</returns>
        public async Task<bool> DeleteReturnDetailsBL(Guid deleteReturnID, Guid deleteProductID)
        {
            bool ReturnDetailsDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    ReturnDetailsDeleted = ReturnDetailsDAL.DeleteReturnDetailDAL(deleteReturnID, deleteProductID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return ReturnDetailsDeleted;
        }




        /// <summary>
        /// Calculate the total price of each product.
        /// </summary>
        /// <param name="returnDetail"></param>
        /// <returns>Returns Each product's total price.</returns>

        public decimal CalculateTotalReturnPriceBL(ReturnDetail returnDetail)
        {
            decimal TotalPrice = (decimal)returnDetail.Quantity * returnDetail.UnitPrice;
            return TotalPrice;
        }



        /// <summary>
        /// Calculate total products that are going to be ordered..
        /// </summary>
        /// <param name="returnDetails"></param>
        /// <returns>Return total quantity that will be shipped.</returns>
        public async Task<int> TotalQuantity(List<ReturnDetail> returnDetails)
        {
            int totalQuantity = 0;
            await Task.Run(() =>
            {
                foreach (ReturnDetail item in returnDetails)
                {
                    totalQuantity = (int)item.Quantity + totalQuantity;
                }
            });

            return totalQuantity;
        }


        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((ReturnDetailDAL)ReturnDetailsDAL).Dispose();
        }

    }
}