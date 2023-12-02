using BusinessAccessLayer.Services.Addresses;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPITask.Controllers;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace webApit.tests.Controllers
{
    public class addresscontroller
    {
        private IAddressServices _addressServices;
        private AddressController _addresscontroller;
        public addresscontroller() 
        {
            _addressServices = A.Fake<IAddressServices>();
            _addresscontroller = new AddressController(_addressServices);
        }
        [Fact]
        public void AddressController_test_index()
        {
            string user_id = "123";
            var fakeAddresses = A.Fake<List<AddressViewModel>>(); // Assuming AddressViewModel is your view model
            A.CallTo(() => _addressServices.GetAddressById(user_id)).Returns(Task.FromResult(fakeAddresses));
            var result = _addresscontroller.Index(user_id);

            // Assert
            result.Should().BeOfType < Task<IActionResult>>();


        }
        [Fact]
        public void AddressController_Index_ReturnsNotFound()
        {
            // Arrange
            string userId = "456";
            A.CallTo(() => _addressServices.GetAddressById(userId)).Returns(Task.FromResult<List<AddressViewModel>>(null));

            
            var result = _addresscontroller.Index(userId);

            
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void Address_Controller_GetAddress_ReturnsOk()
        {
            
            Guid addressId = Guid.NewGuid();
            var fakeAddress = A.Fake<AddressViewModel>();

            A.CallTo(() => _addressServices.GetAddress(addressId)).Returns(Task.FromResult(fakeAddress));

          
            var result = _addresscontroller.GetAddress(addressId).Result;

            
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(fakeAddress);
        }

    }

}

