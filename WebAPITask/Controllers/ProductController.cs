using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.Products;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }
        [HttpGet("Products")]
        public async Task<IActionResult> Get() 
        {
           var products = await _productService.GetProducts();
            if(products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }
        [HttpGet("Product"),Authorize]
        public async Task<IActionResult> GetProduct(Guid ProductId)
        {
            var product = await _productService.FindById(ProductId);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpPut("Update"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel model)
        {
            bool success = await _productService.Update(model);
            if (success)
            {
                return Ok(success);
            }
            return BadRequest();
        }
        [HttpPost("Create"),Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateProduct(AddProductViewModel model)
        {
            bool success = await _productService.Create(model);
            if (success)
            {
                return Ok(success);
            }
            return BadRequest();
        }
        [HttpDelete("Delete"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            bool success = await _productService.Delete(productId);
            if (success)
            {
                return Ok(success); 
            }
            return BadRequest();
        }

    }
}
