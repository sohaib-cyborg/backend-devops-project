using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Addresses
{
    public interface IAddressServices
    {
        Task<bool> CreateAddress(string userId, AddAddressViewModel request);
        Task<bool> Delete(Guid addressId);
        Task<AddressViewModel> GetAddress(Guid id);
        Task<List<AddressViewModel>> GetAddressById(string userId);
        Task<bool> Update(Guid id, UpdateAddressViewModel request);
    }
}
