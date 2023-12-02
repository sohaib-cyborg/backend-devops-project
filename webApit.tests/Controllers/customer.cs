using BusinessAccessLayer.Services.Auth;
using DataAccessLayer.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPITask.Controllers;

namespace webApit.tests.Controllers
{
    public class customer
    {
        private IAuthServices _authServices;
        private CustomerController _customerController;
        public customer() 
        {
            _authServices = A.Fake<IAuthServices>();
            _customerController = new CustomerController(_authServices);
        }
        [Fact]
        public void Customer_controller_index_returnSuccess()
        {
            var fakeCustomers = A.Fake<List<CustomerViewModel>>();
            A.CallTo(() => _authServices.GetCustomers()).Returns(Task.FromResult(fakeCustomers));
            var result = _customerController.index();
            result.Should().BeOfType<Task<IActionResult>>();



        }
        [Fact]
        public void Customer_Controller_GetCustomer_Success()
        {
            // Arrange
            string User_id = "175";
            var fakeCustomer = A.Fake<CustomerViewModel>();

            A.CallTo(() => _authServices.GetCustomerById(User_id)).Returns(Task.FromResult(fakeCustomer));

            // Act
            var result = _customerController.GetCustomer(User_id).Result;

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(fakeCustomer);
        }
      
    }
}
