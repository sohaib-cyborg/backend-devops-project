using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.AddressRepo
{
    public class AddressRepository : IAddressRepo
    {
        private readonly AuthAPIDbContext _db;

        public AddressRepository()
        {
            _db = SingletonDbContext.GetDbContext();
        }
        public async Task<bool> CreateAddress(string userId, AddAddressViewModel request)
        {
            try
            {
                Address add = new Address()
                {
                    AddressId = Guid.NewGuid(),
                    UserId = userId,
                    HouseNum = request.HouseNum,
                    Name = request.Name,
                    AreaCode = request.AreaCode,
                    Area = request.Area,
                    City = request.City,
                };
                await _db.Address.AddAsync(add);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid addressId)
        {
            try
            {
                var address = await _db.Address.FindAsync(addressId);
                _db.Address.Remove(address);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AddressViewModel> GetAddress(Guid id)
        {
            try
            {
                var address = await _db.Address.FindAsync(id);
                AddressViewModel viewModel = new AddressViewModel()
                {
                    Id = id,
                    Area = address.Area,
                    City = address.City,
                    HouseNum = address.HouseNum,
                    Name = address.Name,
                    AreaCode = address.AreaCode,
                };
                return viewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AddressViewModel>> GetAddressById(string id)
        {
            var address = await _db.Address.Where(x => x.UserId == id).ToListAsync();
            List<AddressViewModel> addressViewModels = new List<AddressViewModel>(address.Count);
            addressViewModels.AddRange(address.Select(user => new AddressViewModel
            {
                Id = user.AddressId,
                HouseNum = user.HouseNum,
                Name = user.Name,
                AreaCode = user.AreaCode,
                Area = user.Area,
                City = user.City
            }));
            return addressViewModels;
        }

        public async Task<bool> Update(Guid id, UpdateAddressViewModel request)
        {
            try
            {
                var address = await _db.Address.FindAsync(id);
                address.HouseNum = request.HouseNum;
                address.Name = request.Name;
                address.AreaCode = request.AreaCode;
                address.City = request.City;
                address.Area = request.Area;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
