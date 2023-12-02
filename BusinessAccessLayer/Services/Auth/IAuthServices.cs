using System.Drawing;
using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Auth
{
    public interface IAuthServices
    {
        Task<bool> CreateUser(RegisterationModel request);
        Task<AuthResponseViewModel> GenerateTokenString(LoginModel model);
        Task<CustomerViewModel> GetCustomerById(string userId);
        Task<List<CustomerViewModel>> GetCustomers();
        Task<bool> UpdateCustomer(UpdateCustomerViewModel model,string userId);
    }
}
