using Capgemini.GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatOutdoors.Entities;

namespace Capgemini.GreatOutdoors.Contracts.BLContracts
{
    public interface IReturnDetailsBL : IDisposable
    {
        Task<bool> AddReturnDetailsBL(ReturnDetail newReturnDetails);

        Task<List<ReturnDetail>> GetReturnDetailsByReturnIDBL(Guid searchReturnID);
        Task<List<ReturnDetail>> GetReturnDetailsByProductIDBL(Guid searchProductID);


        Task<bool> DeleteReturnDetailsBL(Guid deleteReturnID, Guid deleteProductID);

        

        decimal CalculateTotalReturnPriceBL(ReturnDetail returnDetails);

        Task<int> TotalQuantity(List<ReturnDetail> returnDetails);



    }
}
