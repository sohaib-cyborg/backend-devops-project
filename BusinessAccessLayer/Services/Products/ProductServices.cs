using Microsoft.EntityFrameworkCore;
using System.Net;
using DataAccessLayer.Context;
using DataAccessLayer.DAL.ProductRepo;
using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo _product;

        public ProductServices(IProductRepo product)
        {
            this._product = product;
        }
        public async Task<bool> Create(AddProductViewModel request)
        {
            if(await _product.Create(request))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid ProductId)
        {
            if (await _product.Delete(ProductId))
            {
                return true;
            }
            return false;
        }

        public async Task<ProductViewModel> FindById(Guid id)
        {
            var prod = await _product.FindById(id);
            return prod;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var products = await _product.GetProducts();
            return products;
        }

        public async Task<bool> Update(UpdateProductViewModel request)
        {
            if(await _product.Update(request))
            {
                return true;
            }
            return false;
        }
    }
}
