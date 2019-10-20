using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;

namespace Capgemini.GreatOutdoors.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting retailers from Retailers collection.
    /// </summary>
    public class RetailerDAL : RetailerDALBase, IDisposable
    {
        /// <summary>
        /// Adds new retailer to Retailers collection.
        /// </summary>
        /// <param name="newRetailer">Contains the retailer details to be added.</param>
        /// <returns>Determinates whether the new retailer is added.</returns>
        public override bool AddRetailerDAL(Retailer newRetailer)
        {
            bool retailerAdded = false;
            try
            {
                newRetailer.RetailerID = Guid.NewGuid();
                newRetailer.CreationDateTime = DateTime.Now;
                newRetailer.LastModifiedDateTime = DateTime.Now;
                retailerList.Add(newRetailer);
                retailerAdded = true;
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return retailerAdded;
        }

        /// <summary>
        /// Gets all retailers from the collection.
        /// </summary>
        /// <returns>Returns list of all retailers.</returns>
        public override List<Retailer> GetAllRetailersDAL()
        {
            return retailerList;
        }

        /// <summary>
        /// Gets retailer based on RetailerID.
        /// </summary>
        /// <param name="searchRetailerID">Represents RetailerID to search.</param>
        /// <returns>Returns Retailer object.</returns>
        public override Retailer GetRetailerByRetailerIDDAL(Guid searchRetailerID)
        {
            Retailer matchingRetailer = null;
            try
            {
                //Find Retailer based on searchRetailerID
                matchingRetailer = retailerList.Find(
                    (item) => { return item.RetailerID == searchRetailerID; }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets retailer based on RetailerName.
        /// </summary>
        /// <param name="retailerName">Represents RetailerName to search.</param>
        /// <returns>Returns Retailer object.</returns>
        public override List<Retailer> GetRetailersByNameDAL(string retailerName)
        {
            List<Retailer> matchingRetailers = new List<Retailer>();
            try
            {
                //Find All Retailers based on retailerName
                matchingRetailers = retailerList.FindAll(
                    (item) => { return item.RetailerName.Equals(retailerName, StringComparison.OrdinalIgnoreCase); }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingRetailers;
        }

        /// <summary>
        /// Gets retailer based on email.
        /// </summary>
        /// <param name="email">Represents Retailer's Email Address.</param>
        /// <returns>Returns Retailer object.</returns>
        public override Retailer GetRetailerByEmailDAL(string email)
        {
            Retailer matchingRetailer = null;
            try
            {
                //Find Retailer based on Email and Password
                matchingRetailer = retailerList.Find(
                    (item) => { return item.Email.Equals(email); }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets retailer based on Email and Password.
        /// </summary>
        /// <param name="email">Represents Retailer's Email Address.</param>
        /// <param name="password">Represents Retailer's Password.</param>
        /// <returns>Returns Retailer object.</returns>
        public override Retailer GetRetailerByEmailAndPasswordDAL(string email, string password)
        {
            Retailer matchingRetailer = null;
            try
            {
                //Find Retailer based on Email and Password
                matchingRetailer = retailerList.Find(
                    (item) => { return item.Email.Equals(email) && item.Password.Equals(password); }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Updates retailer based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Retailer details including RetailerID, RetailerName etc.</param>
        /// <returns>Determinates whether the existing retailer is updated.</returns>
        public override bool UpdateRetailerDAL(Retailer updateRetailer)
        {
            bool retailerUpdated = false;
            try
            {
                //Find Retailer based on RetailerID
                Retailer matchingRetailer = GetRetailerByRetailerIDDAL(updateRetailer.RetailerID);

                if (matchingRetailer != null)
                {
                    //Update retailer details
                    ReflectionHelpers.CopyProperties(updateRetailer, matchingRetailer, new List<string>() { "RetailerName", "RetailerMobile", "Email" });
                    matchingRetailer.LastModifiedDateTime = DateTime.Now;

                    retailerUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return retailerUpdated;
        }

        /// <summary>
        /// Deletes retailer based on RetailerID.
        /// </summary>
        /// <param name="deleteRetailerID">Represents RetailerID to delete.</param>
        /// <returns>Determinates whether the existing retailer is updated.</returns>
        public override bool DeleteRetailerDAL(Guid deleteRetailerID)
        {
            bool retailerDeleted = false;
            try
            {
                //Find Retailer based on searchRetailerID
                Retailer matchingRetailer = retailerList.Find(
                    (item) => { return item.RetailerID == deleteRetailerID; }
                );

                if (matchingRetailer != null)
                {
                    //Delete Retailer from the collection
                    retailerList.Remove(matchingRetailer);
                    retailerDeleted = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return retailerDeleted;
        }

        /// <summary>
        /// Updates retailer's password based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Retailer details including RetailerID, Password.</param>
        /// <returns>Determinates whether the existing retailer's password is updated.</returns>
        public override bool UpdateRetailerPasswordDAL(Retailer updateRetailer)
        {
            bool passwordUpdated = false;
            try
            {
                //Find Retailer based on RetailerID
                Retailer matchingRetailer = GetRetailerByRetailerIDDAL(updateRetailer.RetailerID);

                if (matchingRetailer != null)
                {
                    //Update retailer details
                    ReflectionHelpers.CopyProperties(updateRetailer, matchingRetailer, new List<string>() { "Password" });
                    matchingRetailer.LastModifiedDateTime = DateTime.Now;

                    passwordUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return passwordUpdated;
        }

        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}



