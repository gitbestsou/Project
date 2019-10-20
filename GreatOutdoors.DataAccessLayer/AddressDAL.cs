//using System;
//using System.Collections.Generic;
//using Capgemini.GreatOutdoors.Exceptions;
//using Capgemini.GreatOutdoors.Helpers;
//using Capgemini.GreatOutdoors.Entities;
//using Capgemini.GreatOutdoors.Contracts.DALContracts;

//namespace Capgemini.GreatOutdoors.DataAccessLayer
//{
//    /// <summary>
//    /// Contains data access layer methods for inserting, updating, deleting admins from Admins collection.
//    /// </summary>
//    public class AddressDAL : AddressDALBase, IDisposable
//    {
//        ///// <summary>
//        ///// Constructor of AddressDAL
//        ///// </summary>
//        //public AddressDAL()
//        //{
//        //    Serialize();
//        //    Deserialize();
//        //}

//        /// <summary>
//        /// Adds new address to Address collection.
//        /// </summary>
//        /// <param name="newAddress">Contains the address details to be added.</param>
//        /// <returns>Determinates whether the new address is added.</returns>
//        public override bool AddAddressDAL(Address newAddress)
//        {
//            bool addressAdded = false;
//            try
//            {
//                newAddress.AddressID = Guid.NewGuid();
//                newAddress.CreationDateTime = DateTime.Now;
//                newAddress.LastModifiedDateTime = DateTime.Now;
//                addressList.Add(newAddress);
//                addressAdded = true;
//            }
//            catch (GreatOutdoorsException ex)
//            {
//                throw new GreatOutdoorsException(ex.Message);
//            }
//            return addressAdded;
//        }
//        /// <summary>
//        /// Gets all addresses from the collection.
//        /// </summary>
//        /// <returns>Returns list of all distributors.</returns>
//        public override List<Address> GetAllAddressDAL()
//        {
//            return addressList;
//        }
//        /// <summary>
//        /// Gets distributor based on DistributorID.
//        /// </summary>
//        /// <param name="searchAddressID">Represents DistributorID to search.</param>
//        /// <returns>Returns Distributor object.</returns>
//        public override Address GetAddressByAddressIDDAL(Guid searchAddressID)
//        {
//            Address matchingAddress = null;
//            try
//            {
//                //Find Address based on searchAddressID
//                matchingAddress = addressList.Find(
//                    (item) => { return item.AddressID == searchAddressID; }
//                );
//            }
//            catch (GreatOutdoorsException ex)
//            {
//                throw new GreatOutdoorsException(ex.Message);
//            }
//            return matchingAddress;
//        }

//        /// <summary>
//        /// Updates address based on AddressID.
//        /// </summary>
//        /// <param name="updateAddress">Represents Address details including AddressID, Line1, etc.</param>
//        /// <returns>Determinates whether the existing address is updated.</returns>
//        public override bool UpdateAddressDAL(Address updateAddress)
//        {
//            bool addressUpdated = false;
//            try
//            {
//                //Find address based on AddressID
//                Address matchingAddress = GetAddressByAddressIDDAL(updateAddress.AddressID);

//                if (matchingAddress != null)
//                {
//                    //Update address details
//                    ReflectionHelpers.CopyProperties(updateAddress, matchingAddress, new List<string>() { "Line1", "Line2", "City", "Pincode", "State", "MobileNo" });
//                    matchingAddress.LastModifiedDateTime = DateTime.Now;

//                    addressUpdated = true;
//                }
//            }
//            catch (GreatOutdoorsException ex)
//            {
//                throw new GreatOutdoorsException(ex.Message);
//            }
//            return addressUpdated;

//        }

//        /// <summary>
//        /// Deletes address based on AddressID.
//        /// </summary>
//        /// <param name="deleteAddressID">Represents AddressID to delete.</param>
//        /// <returns>Determinates whether the existing address is updated.</returns>
//        public override bool DeleteAddressDAL(Guid deleteAddressID)
//        {
//            bool addressDeleted = false;
//            try
//            {
//                Address matchingAddress = null;
//                //Find Address based on searchAddressID
//                matchingAddress = addressList.Find(
//                   (item) => { return item.AddressID == deleteAddressID; }
//               );

//                if (matchingAddress != null)
//                {
//                    //Delete Address for collection
//                    addressList.Remove(matchingAddress);
//                    addressDeleted = true;
//                }
//            }
//            catch (GreatOutdoorsException ex)
//            {
//                throw new GreatOutdoorsException(ex.Message);
//            }
//            return addressDeleted;

//        }

//        /// <summary>
//        /// Clears unmanaged resources such as db connections or file streams.
//        /// </summary>
//        public void Dispose()
//        {
//            //No unmanaged resources currently
//        }
//    }
//}


using System;
using System.Collections.Generic;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Contracts.DALContracts;

namespace Capgemini.GreatOutdoors.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting admins from Admins collection.
    /// </summary>
    public class AddressDAL : AddressDALBase, IDisposable
    {
        ///// <summary>
        ///// Constructor of AddressDAL
        ///// </summary>
        //public AddressDAL()
        //{
        //    Serialize();
        //    Deserialize();
        //}

        /// <summary>
        /// Adds new address to Address collection.
        /// </summary>
        /// <param name="newAddress">Contains the address details to be added.</param>
        /// <returns>Determinates whether the new address is added.</returns>
        public override bool AddAddressDAL(Address newAddress)
        {
            bool addressAdded = false;
            try
            {
                newAddress.AddressID = Guid.NewGuid();
                newAddress.CreationDateTime = DateTime.Now;
                newAddress.LastModifiedDateTime = DateTime.Now;
                addressList.Add(newAddress);
                addressAdded = true;
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return addressAdded;
        }
        /// <summary>
        /// Gets all addresses from the collection.
        /// </summary>
        /// <returns>Returns list of all distributors.</returns>
        public override List<Address> GetAllAddressDAL()
        {
            return addressList;
        }
        /// <summary>
        /// Gets distributor based on DistributorID.
        /// </summary>
        /// <param name="searchAddressID">Represents DistributorID to search.</param>
        /// <returns>Returns Distributor object.</returns>
        public override List<Address> GetAddressByRetailerIDDAL(Guid searchRetailerID)
        {
            List<Address> matchingAddress = new List<Address>();
            try
            {
                //Find Address based on searchAddressID
                //matchingAddress = addressList.FindAll(
                //    (item) => { return item.RetailerID == searchRetailerID; }
                //);
               
                foreach (var item in addressList)
                {
                    if (item.RetailerID == searchRetailerID)
                    {
                        matchingAddress.Add(item);
                    }
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return matchingAddress;
        }

        /// <summary>
        /// Updates address based on AddressID.
        /// </summary>
        /// <param name="updateAddress">Represents Address details including AddressID, Line1, etc.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public override bool UpdateAddressDAL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {
                //Find address based on AddressID
                List<Address> matchingAddress = GetAddressByRetailerIDDAL(updateAddress.RetailerID);

                if (matchingAddress != null)
                {
                    //Update address details
                    updateAddress.LastModifiedDateTime = DateTime.Now;
                    ReflectionHelpers.CopyProperties(updateAddress, matchingAddress, new List<string>() { "Line1", "Line2", "City", "Pincode", "State", "MobileNo","LastModifiedDateTime" });
                    //matchingAddress.LastModifiedDateTime = DateTime.Now;

                    addressUpdated = true;
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw new GreatOutdoorsException(ex.Message);
            }
            return addressUpdated;

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
                Address matchingAddress = null;
                //Find Address based on searchAddressID
                matchingAddress = addressList.Find(
                   (item) => { return item.AddressID == deleteAddressID; }
               );

                if (matchingAddress != null)
                {
                    //Delete Address for collection
                    addressList.Remove(matchingAddress);
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
            //No unmanaged resources currently
        }
    }
}
