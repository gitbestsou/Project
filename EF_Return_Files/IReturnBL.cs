using GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.Contracts.BLContracts
{
    public interface IReturnBL : IDisposable
    {
        Task<(bool,Guid)> AddReturnBL(Return newReturn);
        Task<List<Return>> GetAllReturnsBL();
        Task<Return> GetReturnByReturnIDBL(Guid searchReturnID);
        Task<List<Return>> GetReturnByOrderIDBL(Guid OrderID);
        /*Task<List<Return>> GetReturnByRetailerIDBL(Guid RetailerID);*/
        Task<bool> UpdateReturnBL(Return updateReturn);
        Task<bool> DeleteReturnBL(Guid deleteReturnID);

    }
}
