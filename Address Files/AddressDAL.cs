using System;
using System.Collections.Generic;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using GreatOutdoors.Entities;
using System.Linq;

namespace Capgemini.GreatOutdoors.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting admins from Admins collection.
    /// </summary>
    public class AddressDAL : AddressDALBase, IDisposable
    {
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.dbconn);


        TeamAEntities entities = new TeamAEntities();
        
        
        /// <summary>
        /// Adds new address to Address collection.
        /// </summary>
        /// <param name="newAddress">Contains the address details to be added.</param>
        /// <returns>Determinates whether the new address is added.</returns>
        public override (Guid,bool) AddAddressDAL(Address newAddress)
        {
            bool addressAdded = false;
            try
            {
                if (newAddress != null)
                {
                    newAddress.AddressID = Guid.NewGuid();
                    newAddress.CreationDateTime = DateTime.Now;
                    newAddress.LastModifiedDateTime = DateTime.Now;
                    entities.AddAddress(newAddress.AddressID, newAddress.RetailerID, newAddress.Line1, newAddress.Line2, newAddress.City, newAddress.Pincode, newAddress.State, newAddress.MobileNo, newAddress.CreationDateTime, newAddress.LastModifiedDateTime);
                    addressAdded = true; 
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return (newAddress.AddressID,addressAdded);
        }


        /// <summary>
        /// Gets all addresses from the collection.
        /// </summary>
        /// <returns>Returns list of all distributors.</returns>
        public override List<Address> GetAllAddressDAL()
        {
            List<Address> addresses = new List<Address>();
            try
            {
                addresses = entities.Addresses.ToList();
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            
            return addresses;
        }


        /// <summary>
        /// Gets distributor based on DistributorID.
        /// </summary>
        /// <param name="searchAddressID">Represents DistributorID to search.</param>
        /// <returns>Returns Distributor object.</returns>
        public override List<Address> GetAddressByRetailerIDDAL(Guid searchRetailerID)
        {
            List<Address> retailerAddresses = new List<Address>();
            try
            {
                //Find Address based on searchRetailerID
                retailerAddresses = entities.GetAddressByRetailerID(searchRetailerID).ToList();
            }
            catch (GreatOutdoorsException)
            {
                throw new GreatOutdoorsException("Exceptio fetched! Address not found");
            }
            return retailerAddresses;
        }

        /// <summary>
        /// Gets distributor based on DistributorID.
        /// </summary>
        /// <param name="searchAddressID">Represents DistributorID to search.</param>
        /// <returns>Returns Distributor object.</returns>
        public override Address GetAddressByAddressIDDAL(Guid searchAddressID)
        {
            Address retailerAddress = new Address();
            try
            {
                //Find Address based on searchRetailerID
                retailerAddress = entities.GetAddressByAddressID(searchAddressID).FirstOrDefault();
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return retailerAddress;
        }

        /// <summary>
        /// Updates address based on AddressID.
        /// </summary>
        /// <param name="updateAddress">Represents Address details including AddressID, Line1, etc.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public override (Guid,bool) UpdateAddressDAL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {
                //Find address based on AddressID
                if (updateAddress != null)
                {
                    //Update address details
                    updateAddress.LastModifiedDateTime = DateTime.Now;
                    entities.UpdateAddress(updateAddress.AddressID, updateAddress.RetailerID, updateAddress.Line1, updateAddress.Line2, updateAddress.City, updateAddress.Pincode, updateAddress.State, updateAddress.MobileNo, updateAddress.CreationDateTime, updateAddress.LastModifiedDateTime);
                    addressUpdated = true;
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return (updateAddress.AddressID,addressUpdated);

        }

        /// <summary>
        /// Deletes address based on AddressID.
        /// </summary>
        /// <param name="deleteAddressID">Represents AddressID to delete.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public override bool DeleteAddressDAL(Guid deleteAddressID)
        {
            bool addressDeleted = false;
            try
            {
                
                if (deleteAddressID != null)
                {
                    //Delete Address for collection
                    entities.DeleteAddress(deleteAddressID);
                    entities.SaveChanges();
                    addressDeleted = true;
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return addressDeleted;

        }

        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            entities.Dispose();
            //No unmanaged resources currently
        }
    }
}
