using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatOutdoors.Entities;
namespace Capgemini.GreatOutdoors.DAL
{

    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OrderDetailss from OrderDetailss collection.
    /// </summary>
    public class ReturnDetailDAL : ReturnDetailDALBase, IDisposable
    {
        /// <summary>
        /// Adds new OrderDetails to OrderDetails collection.
        /// </summary>
        /// <param name="newReturnDetails">Contains the ReturnDetails details to be added.</param>
        /// <returns>Determinates whether the new ReturnDetails is added.</returns>
        /// 
        TeamAEntities entities = new TeamAEntities();
        public override bool AddReturnDetailDAL(ReturnDetail newReturnDetail)
        {
            bool returnDetailsAdded = false;
            try
            {
                newReturnDetail.ReturnDetailID = Guid.NewGuid();
                entities.AddReturnDetails(newReturnDetail.ReturnDetailID, newReturnDetail.ReturnID, newReturnDetail.ProductID, newReturnDetail.Quantity, Convert.ToString(newReturnDetail.ReasonOfReturn), Convert.ToInt32(newReturnDetail.UnitPrice), Convert.ToInt32(newReturnDetail.TotalPrice), newReturnDetail.AddressID);


                returnDetailsAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return returnDetailsAdded;
        }


        /// <summary>
        /// Gets OrderDetails based on OrderID.
        /// </summary>
        /// <param name="searchReturnID">Represents OrderDetailsID to search.</param>
        /// <returns>Returns List of OrderDetails object.</returns>
        public override List<ReturnDetail> GetReturnDetailByReturnIDDAL(Guid searchReturnID)
        {
            List<ReturnDetail> matchingReturnDetails = new List<ReturnDetail>();
            try
            {
                matchingReturnDetails = entities.GetReturnDetailsByReturnID(searchReturnID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        }

        /// <summary>
        /// Gets OrderDetails based on ProductID.
        /// </summary>
        /// <param name="searchProductID">Represents ReturnDetailsID to search.</param>
        /// <returns>Returns OrderDetails object.</returns>
        public override List<ReturnDetail> GetReturnDetailByProductIDDAL(Guid searchProductID)
        {
            List<ReturnDetail> matchingReturnDetails = new List<ReturnDetail>();
            try
            {
                matchingReturnDetails = entities.GetReturnDetailsByProductID(searchProductID).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturnDetails;
        }



        /// <summary>
        /// Deletes OrderDetails based on OrderID and ProductID.
        /// </summary>
        /// <param name="deleteOrderID">Represents OrderDetailsID to delete.</param>
        /// <param name="deleteProductID">Represents OrderDetailsID to delete.</param>
        /// <returns>Determinates whether the existing OrderDetails is updated.</returns>
        public override bool DeleteReturnDetailDAL(Guid deleteReturnID, Guid deleteProductID)
        {
            bool returnDetailsDeleted = false;
            try
            {

                if (deleteReturnID != null)
                {
                    entities.DeleteReturn(deleteReturnID);
                    entities.SaveChanges();
                    returnDetailsDeleted = true;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return returnDetailsDeleted;
        }





        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
            entities.Dispose();
        }
    }
}
