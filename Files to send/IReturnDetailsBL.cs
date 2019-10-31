using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreatOutdoors.Entities;

namespace Capgemini.GreatOutdoors.Contracts.BLContracts
{
    public interface IReturnDetailsBL : IDisposable
    {
        Task<bool> AddReturnDetailsBL(ReturnDetail newReturnDetails);
        Task<List<ReturnDetail>> GetAllReturnDetailsBL();
        Task<List<ReturnDetail>> GetReturnDetailsByReturnIDBL(Guid searchReturnID);
        Task<List<ReturnDetail>> GetReturnDetailsByProductIDBL(Guid searchProductID);
        Task<List<ReturnDetail>> GetReturnDetailsByRetailerIDBL(Guid searchRetailerID);

        Task<bool> DeleteReturnDetailsBL(Guid deleteReturnID, Guid deleteProductID);



        decimal CalculateTotalReturnPriceBL(ReturnDetail returnDetails);

        Task<int> TotalQuantity(List<ReturnDetail> returnDetails);



    }
}