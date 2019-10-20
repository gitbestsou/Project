using System;
using System.Collections.Generic;
using System.Linq;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;
using GreatOutdoors.Entities;
namespace Capgemini.GreatOutdoors.DAL
{
    public class ReturnDAL : ReturnDALBase, IDisposable
    {
        TeamAEntities entities = new TeamAEntities();

        public ReturnDAL()
        {

        }
        public override (bool, Guid) AddReturnDAL(Return newReturn)
        {
            
            bool returnAdded = false;
            Guid returnID = default(Guid);
            try
            {
                /*
                 public System.Guid ReturnID { get; set; }
                public Nullable<System.Guid> OrderID { get; set; }
                public string ChannelOfReturn { get; set; }
                public decimal ReturnAmount { get; set; }
                public System.DateTime ReturnDateTime { get; set; }
                public System.DateTime LastModifiedDateTime { get; set; }
    
                 */
                newReturn.ReturnID = Guid.NewGuid();
                newReturn.ReturnDateTime = DateTime.Now;
                newReturn.LastModifiedDateTime = DateTime.Now;
                entities.AddReturn(returnID,newReturn.OrderID,newReturn.ChannelOfReturn,newReturn.ReturnAmount,newReturn.ReturnDateTime,newReturn.LastModifiedDateTime);
                entities.SaveChanges();
                returnAdded = true;
            }
            catch (GreatOutdoorsException ex)
            {
                throw new Exception(ex.Message);
            }
            return (returnAdded, returnID);

        }

        public override List<Return> GetAllReturnsDAL()
        {

            List<Return> returns = new List<Return>();
            returns = entities.Returns.ToList();
            return returns;
        }

        public override Return GetReturnByReturnIDDAL(Guid searchReturnID)
        {
            Return matchingReturn = null;
            try
            {

                /*matchingReturn = returnList.Find(
                    (item) => { return item.ReturnID == searchReturnID; }
                );*/

                matchingReturn = entities.Returns.Find(searchReturnID);
                if(matchingReturn==null)
                {
                    throw new GreatOutdoorsException("Invalid Return ID");
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingReturn;
        }
        public override List<Return> GetReturnByOrderIDDAL(Guid OrderID)
        {
            List<Return> matchingReturns = null;
            /*Sourav will have to write this code*/

            try
            {

                matchingReturns = entities.Returns.ToList();
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingReturns;

        }
        
        public override bool UpdateReturnDAL(Return updateReturn)
        {
            bool returnUpdated = false;
            try
            {
                //Find Distributor based on DistributorID
                Return matchingReturn = GetReturnByReturnIDDAL(updateReturn.ReturnID);

                if (matchingReturn != null)
                {

                    /*ReflectionHelpers.CopyProperties(updateReturn, matchingReturn, new List<string>() { "ReturnIncomplete", "ReturnWrong", "ReturnValue", "ReturnQuantity" });
                    matchingReturn.ReturnDateTime = DateTime.Now;*/
                    matchingReturn.OrderID = updateReturn.OrderID;
                    matchingReturn.ChannelOfReturn = updateReturn.ChannelOfReturn;
                    matchingReturn.ReturnAmount = updateReturn.ReturnAmount;
                    matchingReturn.ReturnDateTime = updateReturn.ReturnDateTime;
                    matchingReturn.LastModifiedDateTime = DateTime.Now;
                    entities.SaveChanges();
                    returnUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return returnUpdated;
        }

        public override bool DeleteReturnDAL(Guid deleteReturnID)
        {
            bool returnDeleted = false;
            try
            {
                if(deleteReturnID!=null)
                {
                    entities.DeleteReturn(deleteReturnID);
                    entities.SaveChanges();
                    returnDeleted = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return returnDeleted;
        }

        public void Dispose()
        {
            entities.Dispose();
        }





    }
}
