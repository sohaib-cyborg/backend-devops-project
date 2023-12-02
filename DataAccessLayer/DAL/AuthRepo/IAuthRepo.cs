using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.AuthRepo
{
    public interface IAuthRepo
    {
        Task<bool> CreateUser(RegisterationModel request);
        Task<ApplicationUser> FindByEmail(string email);
        Task<IList<string>> GetRoles(ApplicationUser user);
        Task<CustomerViewModel> GetCustomerById(string userId);
        Task<List<CustomerViewModel>> GetCustomers();
        Task<bool> UpdateCustomer(UpdateCustomerViewModel model, string userId);
        Task<bool> UserLogin(LoginModel model);
    }
}
