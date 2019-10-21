using System;
using System.Collections.Generic;
using System.IO;
using Capgemini.GreatOutdoors.Entities;
using GreatOutdoors.Entities;

using Newtonsoft.Json;

namespace Capgemini.GreatOutdoors.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for OrderDetailsDAL class
    /// </summary>
    public abstract class ReturnDetailDALBase
    {
        //Collection of OrderDetails
        protected static List<ReturnDetail> ReturnDetailList = new List<ReturnDetail>();
        private static string fileName = "ReturnDetail.json";

        //Methods for CRUD operations
        public abstract bool AddReturnDetailDAL(ReturnDetail newReturnDetail);

        public abstract List<ReturnDetail> GetReturnDetailByReturnIDDAL(Guid searchReturnID);
        public abstract List<ReturnDetail> GetReturnDetailByProductIDDAL(Guid searchProductID);

        public abstract bool DeleteReturnDetailDAL(Guid deleteReturnID, Guid deleteProductID);


        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(ReturnDetailList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }

        /// <summary>
        /// Reads collection from the file in JSON format.
        /// </summary>
        public static void Deserialize()
        {
            string fileContent = string.Empty;
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var systemUserListFromFile = JsonConvert.DeserializeObject<List<ReturnDetail>>(fileContent);
                if (systemUserListFromFile != null)
                {
                    ReturnDetailList = systemUserListFromFile;
                }
            }
        }


    }
}


