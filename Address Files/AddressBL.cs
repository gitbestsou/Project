using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoors.BusinessLayer;
using Capgemini.GreatOutdoors.Contracts.BLContracts;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.DataAccessLayer;
using GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;

namespace Capgemini.GreatOutdoors.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting Addresss from Addresss collection.
    /// </summary>
    public class AddressBL : BLBase<Address>, IAddressBL, IDisposable
    {
        //fields
        AddressDALBase addressDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddressBL()
        {
            this.addressDAL = new AddressDAL();
        }

        /// <summary>
        /// Adds new address to Address collection.
        /// </summary>
        /// <param name="newAddress">Contains the address details to be added.</param>
        /// <returns>Determinates whether the new address is added.</returns>
        public async Task<bool> AddAddressBL(Address newAddress)
        {
            bool addressAdded = false ;
            try
            {
                if (await Validate(newAddress))
                {
                   addressDAL.AddAddressDAL(newAddress);
                    addressAdded = true;
                    //await Task.Run(() =>
                    //{
                    //    this.addressDAL.AddAddressDAL(newAddress);
                    //    addressAdded = true;
                    //    //Serialize();
                    //});
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return addressAdded;
        }

        /// <summary>
        /// Gets all address from the collection.
        /// </summary>
        /// <returns>Returns list of all addresses.</returns>
        public async Task<List<Address>> GetAllAddressBL()
        {
            List<Address> addressList = null;
            try
            {
                await Task.Run(() =>
                {
                    addressList = addressDAL.GetAllAddressDAL();
                });
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return addressList;
        }

        /// <summary>
        /// Gets address based on AddressID.
        /// </summary>
        /// <param name="searchAddressID">Represents AddressID to search.</param>
        /// <returns>Returns Address object.</returns>
        public async Task<List<Address>> GetAddressByRetailerIDBL(Guid searchRetailerID)
        {
            List<Address> matchingAddress = new List<Address>();
            try
            {
                //matchingAddress = addressDAL.GetAddressByRetailerIDDAL(searchRetailerID);
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByRetailerIDDAL(searchRetailerID);
                });
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingAddress;
        }

        /// <summary>
        /// Gets address based on AddressID.
        /// </summary>
        /// <param name="searchAddressID">Represents AddressID to search.</param>
        /// <returns>Returns Address object.</returns>
        public async Task<Address> GetAddressByAddressIDBL(Guid searchAddressID)
        {
            Address matchingAddress = new Address();
            try
            {
                //matchingAddress = addressDAL.GetAddressByRetailerIDDAL(searchRetailerID);
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByAddressIDDAL(searchAddressID);
                });
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingAddress;
        }

        /// <summary>
        /// Updates address based on AddressID.
        /// </summary>
        /// <param name="updateAddress">Represents address details including AddressID, AddressName etc.</param>
        /// <returns>Determinates whether the existing Address is updated.</returns>
        public async Task<bool> UpdateAddressBL(Address updateAddress)
        {
            bool AddressUpdated = false;
            try
            {
                if ((await Validate(updateAddress)) && (GetAddressByRetailerIDBL(updateAddress.AddressID)) != null)
                {
                    this.addressDAL.UpdateAddressDAL(updateAddress);
                    AddressUpdated = true;
                    //Serialize();
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return AddressUpdated;
        }

        /// <summary>
        /// Deletes address based on AddressID.
        /// </summary>
        /// <param name="deleteAddressID">Represents AddressID to delete.</param>
        /// <returns>Determinates whether the existing Address is updated.</returns>
        public async Task<bool> DeleteAddressBL(Guid deleteAddressID)
        {
            bool AddressDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    AddressDeleted = addressDAL.DeleteAddressDAL(deleteAddressID);
                    //Serialize();
                });
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return AddressDeleted;
        }



        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((AddressDAL)addressDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        //public static void Serialize()
        //{
        //    try
        //    {
        //        AddressDAL.Serialize();
        //    }
        //    catch (GreatOutdoorsException)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        ///Invokes Deserialize method of DAL.
        /// </summary>
        //public static void Deserialize()
        //{
        //    try
        //    {
        //        AddressDAL.Deserialize();
        //    }
        //    catch (GreatOutdoorsException)
        //    {
        //        throw;
        //    }
        //}
    }
}
