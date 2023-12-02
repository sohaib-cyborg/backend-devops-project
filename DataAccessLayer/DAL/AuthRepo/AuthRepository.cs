using Azure.Core;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Constants;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.AuthRepo
{
    public class AuthRepository : IAuthRepo
    {
        private readonly AuthAPIDbContext _db;
        private readonly UserManager<ApplicationUser> _UserManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            _db = SingletonDbContext.GetDbContext();
            _UserManager = userManager;
        }

        public async Task<bool> CreateUser(RegisterationModel request)
        {
            var identityUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _UserManager.CreateAsync(identityUser, request.Password);
            await _UserManager.AddToRoleAsync(identityUser, Roles.Customer.ToString());
            return result.Succeeded;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            var user = await _UserManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<CustomerViewModel> GetCustomerById(string userId)
        {
            var customer = await _UserManager.FindByIdAsync(userId);
            CustomerViewModel model = new CustomerViewModel()
            {
                UserId = userId,
                CustomerName = customer.Name,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.PhoneNumber
            };
            return model;
        }

        public async Task<List<CustomerViewModel>> GetCustomers()
        {
            var customers = await _UserManager.GetUsersInRoleAsync("Customer");
            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>(customers.Count);
            customerViewModels.AddRange(customers.Select(user => new CustomerViewModel
            {
                UserId = user.Id,
                CustomerEmail = user.Email,
                CustomerName = user.Name,
                CustomerPhone = user.PhoneNumber
            }));
            return customerViewModels;
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            var roles = await _UserManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<bool> UpdateCustomer(UpdateCustomerViewModel model, string userId)
        {
            var customer = await _UserManager.FindByIdAsync(userId);
            if (customer == null)
            {
                return false;
            }
            customer.Name = model.CustomerName;
            customer.Email = model.CustomerEmail;
            customer.UserName = model.CustomerEmail;
            customer.PhoneNumber = model.CustomerPhone;
            await _UserManager.UpdateAsync(customer);

            return true;
        }

        public async Task<bool> UserLogin(LoginModel model)
        {
            var identityUser = await _UserManager.FindByEmailAsync(model.Email);
            if (identityUser is not null)
            {
                return await _UserManager.CheckPasswordAsync(identityUser, model.Password);
            }
            return false;
        }
    }
}
