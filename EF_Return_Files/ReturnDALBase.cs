using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatOutdoors.Entities;
using Newtonsoft.Json;
using Capgemini.GreatOutdoors.Entities;

namespace Capgemini.GreatOutdoors.Contracts.DALContracts
{
    public abstract class ReturnDALBase
    {
        protected static List<Return> returnList = new List<Return>();
        private static string fileName = "return.json";

        public abstract (bool,Guid) AddReturnDAL(Return newReturn);
        public abstract List<Return> GetAllReturnsDAL();
        public abstract Return GetReturnByReturnIDDAL(Guid searchReturnID);
        public abstract List<Return> GetReturnByOrderIDDAL(Guid OrderID);
        /*public abstract List<Return> GetReturnByRetailerIDDAL(Guid RetailerID);*/
        public abstract bool UpdateReturnDAL(Return updateReturn);
        public abstract bool DeleteReturnDAL(Guid deleteReturnID);



        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(returnList);
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
                var systemUserListFromFile = JsonConvert.DeserializeObject<List<Return>>(fileContent);
                if (systemUserListFromFile != null)
                {
                    returnList = systemUserListFromFile;
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static ReturnDALBase()
        {
            Deserialize();
        }


    }
}
