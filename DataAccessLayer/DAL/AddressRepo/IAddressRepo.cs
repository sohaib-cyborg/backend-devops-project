using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.AddressRepo
{
    public interface IAddressRepo
    {
        Task<bool> CreateAddress(string userId, AddAddressViewModel request);
        Task<bool> Delete(Guid addressId);
        Task<AddressViewModel> GetAddress(Guid id);
        Task<List<AddressViewModel>> GetAddressById(string userId);
        Task<bool> Update(Guid id, UpdateAddressViewModel request);
    }
}
