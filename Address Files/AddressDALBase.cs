using System;
using System.Collections.Generic;
using System.IO;
using GreatOutdoors.Entities;
using Newtonsoft.Json;

namespace Capgemini.GreatOutdoors.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for AddressDAL class
    /// </summary>
    public abstract class AddressDALBase
    {
       //Methods for CRUD operations
        public abstract (Guid,bool) AddAddressDAL(Address newAddress);
        public abstract List<Address> GetAllAddressDAL();
        public abstract List<Address> GetAddressByRetailerIDDAL(Guid searchRetailerID);
        public abstract Address GetAddressByAddressIDDAL(Guid searchAddressID);
        public abstract (Guid,bool) UpdateAddressDAL(Address updateAddress);
        public abstract bool DeleteAddressDAL(Guid deleteAddressID);

        ///// <summary>
        ///// Writes collection to the file in JSON format.
        ///// </summary>
        //public static void Serialize()
        //{
        //    string serializedJson = JsonConvert.SerializeObject(addressList);
        //    using (StreamWriter streamWriter = new StreamWriter(fileName))
        //    {
        //        streamWriter.Write(serializedJson);
        //        streamWriter.Close();
        //    }
        //}

        ///// <summary>
        ///// Reads collection from the file in JSON format.
        ///// </summary>
        //public static void Deserialize()
        //{
        //    string fileContent = string.Empty;
        //    if (!File.Exists(fileName))
        //        File.Create(fileName).Close();

        //    using (StreamReader streamReader = new StreamReader(fileName))
        //    {
        //        fileContent = streamReader.ReadToEnd();
        //        streamReader.Close();
        //        var addressListFromFile = JsonConvert.DeserializeObject<List<Address>>(fileContent);
        //        if (addressListFromFile != null)
        //        {
        //            addressList = addressListFromFile;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Static Constructor.
        ///// </summary>
        //static AddressDALBase()
        //{
        //    Deserialize();
        //}
    }
}