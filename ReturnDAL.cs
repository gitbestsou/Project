using System;
using System.Collections.Generic;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Capgemini.GreatOutdoors.DAL
{
    public class ReturnDAL : ReturnDALBase, IDisposable
    {
        SqlConnection sqlConn = new SqlConnection(DataAccessLayer.Properties.Settings.Default.dbConn);
        public override (bool, Guid) AddReturnDAL(Return newReturn)
        {

            bool returnAdded = false;
            Guid returnID = default(Guid);

            try
            {
                /*newReturn.ReturnID = Guid.NewGuid();
                newReturn.ReturnDateTime = DateTime.Now;
                returnList.Add(newReturn);
                returnID = newReturn.ReturnID;
                returnAdded = true;*/
                sqlConn.Open();
                newReturn.ReturnID = returnID;
                string procName = "[13th Aug CLoud PT Immersive].TeamA.AddReturn";
                SqlCommand cmd = new SqlCommand(procName,sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@returnID",newReturn.ReturnID);
                cmd.Parameters.AddWithValue("@orderID", newReturn.OrderID);
                cmd.Parameters.AddWithValue("@returnAmount", newReturn.ReturnAmount);
                cmd.Parameters.AddWithValue("@channnelOfReturn",Convert.ToString(newReturn.ChannelOfReturn));
                
                cmd.Parameters.AddWithValue("@returnDateTime",DateTime.Now);
                cmd.Parameters.AddWithValue("@lastModifiedTime", DateTime.Now);

                int rows = cmd.ExecuteNonQuery();

                if (rows == 1)
                    returnAdded = true;
                sqlConn.Close();
          
            }
            catch (GreatOutdoorsException ex)
            {
                throw new Exception(ex.Message);
            }
            return (returnAdded, returnID);

        }

        public override List<Return> GetAllReturnsDAL()
        {
            //return returnList;


            List<Return> returns = new List<Return>();

            try
            {
                sqlConn.Open();
                string procName = "TeamA.GetAllReturns";
                SqlCommand cmd = new SqlCommand(procName, sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataSet dtSet = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dtSet);

                for( int i = 0;i < dtSet.Tables[0].Rows.Count;i++ )
                {
                    DataRow row = dtSet.Tables[0].Rows[i];
                    Return returnObj = new Return();
                    returnObj.ReturnID = (Guid)row["ReturnID"];
                    returnObj.OrderID = (Guid)row["OrderID"];
                    returnObj.ChannelOfReturn = (ReturnChannel)row["ChannelOfReturn"];
                    returnObj.ReturnAmount = Convert.ToInt32(row["ReturnAmount"]);
                    returnObj.ReturnDateTime = (DateTime)row["ReturnDateTime"];
                    returnObj.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    
                    returns.Add(returnObj);

                }


                sqlConn.Close();
            }

            catch (GreatOutdoorsException ex)
            {
                throw ex;
            }

           return returns;

        }

        public override Return GetReturnByReturnIDDAL(Guid searchReturnID)
        {


            Return matchingReturn = new Return();
            try
            {
                //Find Distributor based on searchDistributorID
                /*matchingReturn = returnList.Find(
                    (item) => { return item.ReturnID == searchReturnID; }
                );*/

                sqlConn.Open();
                string procName = "TeamA.GetReturnByReturnID";
                SqlCommand cmd = new SqlCommand(procName,sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@returnID",searchReturnID);
                SqlDataReader row = cmd.ExecuteReader();
                while(row.Read())
                {
                    matchingReturn.ReturnID = row.GetGuid(0);
                    matchingReturn.OrderID = row.GetGuid(1);
                    matchingReturn.ReturnAmount = row.GetInt32(3);
                    if (row.GetString(4) == "OFfline")
                        matchingReturn.ChannelOfReturn = ReturnChannel.Offline;
                    else
                        matchingReturn.ChannelOfReturn = ReturnChannel.Online;
                    matchingReturn.ReturnAmount = row.GetInt32(4);
                    matchingReturn.ReturnDateTime = row.GetDateTime(5);
                    matchingReturn.LastModifiedDateTime = row.GetDateTime(6);

                }
                row.Close();
                sqlConn.Close();
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingReturn;


        }





        public override List<Return> GetReturnByOrderIDDAL(Guid OrderID)
        {
            List<Return> matchingReturn = null;
            /*Sourav will have to write this code*/

            try
            {
                foreach (Return item in returnList)
                {
                    if (item.OrderID == OrderID)
                    {
                        matchingReturn.Add(item);
                    }
                }
                //Find Distributor based on searchDistributorID
                
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingReturn;

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
                    //Update distributor details
                    ReflectionHelpers.CopyProperties(updateReturn, matchingReturn, new List<string>() { "ReturnIncomplete", "ReturnWrong", "ReturnValue", "ReturnQuantity" });
                    matchingReturn.ReturnDateTime = DateTime.Now;

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
                //Find Distributor based on searchDistributorID
                Return matchingReturn = returnList.Find(
                    (item) => { return item.ReturnID == deleteReturnID; }
                );

                if (matchingReturn != null)
                {
                    //Delete Distributor from the collection
                    returnList.Remove(matchingReturn);
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

        }





    }
}
