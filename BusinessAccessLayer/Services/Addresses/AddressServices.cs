using DataAccessLayer.DAL.AddressRepo;
using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Addresses
{
    public class AddressServices : IAddressServices
    {
        private readonly IAddressRepo _address;

        public AddressServices(IAddressRepo address)
        {
            _address = address;
        }
        public async Task<bool> CreateAddress(string userId, AddAddressViewModel request)
        {
            if(await _address.CreateAddress(userId, request))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid addressId)
        {
            if (await _address.Delete(addressId))
            {
                return true;
            }
            return false;
        }

        public async Task<AddressViewModel> GetAddress(Guid id)
        {
           var address = await _address.GetAddress(id);
            return address;
        }

        public async Task<List<AddressViewModel>> GetAddressById(string id)
        {
            var address = await _address.GetAddressById(id);
            return address;
        }

        public async Task<bool> Update(Guid id, UpdateAddressViewModel request)
        {
            if(await _address.Update(id, request))
            {
                return true;
            }
            return false;
        }
    }
}
