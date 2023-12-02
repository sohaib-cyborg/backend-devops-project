using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using DataAccessLayer.Constants;
using DataAccessLayer.Context;
using DataAccessLayer.DAL.AuthRepo;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessAccessLayer.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepo _auth;
        private readonly IConfiguration _config;

        public AuthServices(IAuthRepo auth,IConfiguration config)
        {
            _auth = auth;
            _config = config;
        }

        public async Task<bool> CreateUser(RegisterationModel request)
        {
            if (await _auth.CreateUser(request))
            {
                return true;
            }
            return false;
        }
        public async Task<AuthResponseViewModel> GenerateTokenString(LoginModel model)
        {
            if (await _auth.UserLogin(model)) {
                var user = await _auth.FindByEmail(model.Email);
                var role = await _auth.GetRoles(user);
                var list1 = role.ToList();
                string userRole = list1[0];
                IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role,userRole)
            };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
                SigningCredentials signCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    issuer: _config.GetSection("Jwt:Issuer").Value,
                    audience: _config.GetSection("Jwt:Audience").Value,
                    signingCredentials: signCred
                    );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                AuthResponseViewModel authResponse = new AuthResponseViewModel()
                {
                    token = tokenString,
                    userId = user.Id
                };
                return authResponse;
            }
            return null;
        }

        public async Task<List<CustomerViewModel>> GetCustomers()
        {
            var customers = await _auth.GetCustomers();
            return customers;
        }

        public async Task<bool> UpdateCustomer(UpdateCustomerViewModel model,string userId)
        {
            if(await _auth.UpdateCustomer(model, userId))
            {
                return true;
            }
            return false;
        }

        public async Task<CustomerViewModel> GetCustomerById(string userId)
        {
            var customer = await _auth.GetCustomerById(userId);
            return customer;
        }
    }
}
