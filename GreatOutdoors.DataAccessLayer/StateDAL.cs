using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Exceptions;
using System;
using System.Collections.Generic;

namespace Capgemini.GreatOutdoors.DAL
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting admins from Admins collection.
    /// </summary>
    public class StateDAL : StateDALBase, IDisposable
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
        /// Adds new state to State collection.
        /// </summary>
        /// <param name="newState">Contains the state details to be added.</param>
        /// <returns>Determinates whether the new state is added.</returns>
        public override bool AddStateDAL(string newState)
        {
            bool stateAdded = false;
            try
            {
                State.Add(newState);
                stateAdded = true;
            }
            catch (GreatOutdoorsException ex)
            {
                throw;
            }
            return stateAdded;
        }
        /// <summary>
        /// Gets all states from the collection.
        /// </summary>
        /// <returns>Returns list of all States.</returns>
        public override List<string> GetAllStateDAL()
        {
            return State;
        }

        /// <summary>
        /// Deletes state.
        /// </summary>
        /// <param name="deleteState">Represents State to delete.</param>
        /// <returns>Determinates whether the existing State is deleted.</returns>
        public override bool DeleteStateDAL(string deleteState)
        {
            bool stateDeleted = false;
            try
            {
                string matchingAddress = null;
                //Find State based on state name
                matchingAddress = State.Find(
                   (item) => { return item == deleteState; }
               );

                if (matchingAddress != null)
                {
                    //Delete State for collection
                    State.Remove(matchingAddress);
                    stateDeleted = true;
                }
            }
            catch (GreatOutdoorsException ex)
            {
                throw;
            }
            return stateDeleted;

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
