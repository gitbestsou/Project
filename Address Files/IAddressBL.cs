//using Capgemini.GreatOutdoors.Entities;
//using GreatOutdoors.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Capgemini.GreatOutdoors.Contracts.BLContracts
//{
//    public interface IAddressBL
//    {
//        Task<bool> AddAddressBL(Address newAddress);
//        Task<List<Address>> GetAllAddressBL();
//        Task<Address> GetAddressByAddressIDBL(Guid searchAddressID);
//        Task<bool> UpdateAddressBL(Address updateAddress);
//        Task<bool> DeleteAddressBL(Guid deleteAddressID);
//    }
//}




using GreatOutdoors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoors.Contracts.BLContracts
{
    public interface IAddressBL : IDisposable
    {
        Task<bool> AddAddressBL(Address newAddress);
        Task<List<Address>> GetAllAddressBL();
        Task<List<Address>> GetAddressByRetailerIDBL(Guid searchRetailerID);
        Task<Address> GetAddressByAddressIDBL(Guid searchAddressID);
        Task<bool> UpdateAddressBL(Address updateAddress);
        Task<bool> DeleteAddressBL(Guid deleteAddressID);
    }
}