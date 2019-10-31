using System;
using System.Collections.Generic;
using System.IO;
using GreatOutdoors.Entities;



namespace Capgemini.GreatOutdoors.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for OrderDetailsDAL class
    /// </summary>
    public abstract class ReturnDetailDALBase
    {
        //Collection of OrderDetails
        protected static List<ReturnDetail> ReturnDetailList = new List<ReturnDetail>();        
        //Methods for CRUD operations
        public abstract bool AddReturnDetailDAL(ReturnDetail newReturnDetail);
        public abstract List<ReturnDetail> GetAllReturnDetailsDAL();

        public abstract List<ReturnDetail> GetReturnDetailByReturnIDDAL(Guid searchReturnID);
        public abstract List<ReturnDetail> GetReturnDetailByProductIDDAL(Guid searchProductID);

        public abstract List<ReturnDetail> GetReturnDetailsByRetailerIDDAL(Guid searchRetailerID);


        public abstract bool DeleteReturnDetailDAL(Guid deleteReturnID, Guid deleteProductID);



    }
}

