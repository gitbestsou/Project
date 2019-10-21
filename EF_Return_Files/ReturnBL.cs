using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.DAL;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using GreatOutdoors.Entities;

namespace Capgemini.GreatOutdoors.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting distributors from Distributors collection.
    /// </summary>
    public class ReturnBL : BLBase<Return>, IReturnBL, IDisposable
    {
        //fields
        ReturnDALBase returnDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReturnBL()
        {
            this.returnDAL = new ReturnDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(Return entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            //Email is Unique
            var existingObject = await GetReturnByReturnIDBL(entityObject.ReturnID);
            if (existingObject != null && existingObject?.ReturnID != entityObject.ReturnID)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"Return {entityObject.ReturnID} already exists");
            }

            if (valid == false)
                throw new GreatOutdoorsException(sb.ToString());
            return valid;
        }

        /// <summary>
        /// Adds new distributor to Distributors collection.
        /// </summary>
        /// <param name="newReturn">Contains the distributor details to be added.</param>
        /// <returns>Determinates whether the new distributor is added.</returns>
        public async Task<(bool,Guid)> AddReturnBL(Return newReturn)
        {
            bool returnAdded = false;
            Guid returnID = default(Guid);
            try
            {
                if (await Validate(newReturn))
                {
                    await Task.Run(() =>
                    {
                        (returnAdded, returnID) = returnDAL.AddReturnDAL(newReturn);
                        returnAdded = true;
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (returnAdded,returnID);
        }

        /// <summary>
        /// Gets all distributors from the collection.
        /// </summary>
        /// <returns>Returns list of all distributors.</returns>
        public async Task<List<Return>> GetAllReturnsBL()
        {
            List<Return> returnList = null;
            try
            {
                await Task.Run(() =>
                {
                    returnList = returnDAL.GetAllReturnsDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return returnList;
        }

        /// <summary>
        /// Gets distributor based on DistributorID.
        /// </summary>
        /// <param name="searchReturnID">Represents DistributorID to search.</param>
        /// <returns>Returns Distributor object.</returns>
        public async Task<Return> GetReturnByReturnIDBL(Guid searchReturnID)
        {
            Return matchingReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingReturn = returnDAL.GetReturnByReturnIDDAL(searchReturnID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingReturn;

        }

        public async Task<List<Return>> GetReturnByOrderIDBL(Guid OrderID)
        {
            List<Return> matchingReturn = null;
            /*Sourav will have to write this code*/
            try
            {
                await Task.Run(async () =>
                {
                    await Task.Run(() => matchingReturn = returnDAL.GetReturnByOrderIDDAL(OrderID));

                });
            }
            catch (Exception)
            {
                throw;
            }

            return matchingReturn;
        }

        /*public async Task<List<Return>> GetReturnByRetailerIDBL(Guid RetailerID)
        {
            List<Return> returnList = null;
            try
            {
                await Task.Run(() =>
                {
                    returnList = returnDAL.GetReturnByRetailerIDDAL(RetailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return returnList;
        }*/



        /// <summary>
        /// Updates distributor based on DistributorID.
        /// </summary>
        /// <param name="updateDistributor">Represents Distributor details including DistributorID, DistributorName etc.</param>
        /// <returns>Determinates whether the existing distributor is updated.</returns>
        public async Task<bool> UpdateReturnBL(Return updateReturn)
        {
            bool returnUpdated = false;
            try
            {
                if ((await Validate(updateReturn)) && (await GetReturnByReturnIDBL(updateReturn.ReturnID)) != null)
                {
                    this.returnDAL.UpdateReturnDAL(updateReturn);
                    returnUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnUpdated;
        }

        /// <summary>
        /// Deletes distributor based on DistributorID.
        /// </summary>
        /// <param name="deleteReturnID">Represents DistributorID to delete.</param>
        /// <returns>Determinates whether the existing distributor is updated.</returns>
        public async Task<bool> DeleteReturnBL(Guid deleteReturnID)
        {
            bool returnDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    returnDeleted = returnDAL.DeleteReturnDAL(deleteReturnID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return returnDeleted;
        }



        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((ReturnDAL)returnDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public static void Serialize()
        {
            try
            {
                ReturnDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///Invokes Deserialize method of DAL.
        /// </summary>
        public static void Deserialize()
        {
            try
            {
                ReturnDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void OrderForReturn(Guid OrderId, Guid ProductId)
        {

        }

        public void CancelOrder(Guid OrderId)
        {

        }

    }
}



