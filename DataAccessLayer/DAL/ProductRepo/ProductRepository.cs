using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.ProductRepo
{
    public class ProductRepository : IProductRepo
    {
        private readonly AuthAPIDbContext _db;

        public ProductRepository()
        {
            this._db = SingletonDbContext.GetDbContext();
        }
        public async Task<bool> Create(AddProductViewModel request)
        {
            try
            {
                Product prod = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Category = request.Category,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Image = request.Image,
                    isAvailable = 1
                };
                await _db.Product.AddAsync(prod);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid ProductId)
        {
            try
            {
                var prod = await _db.Product.FindAsync(ProductId);
                _db.Product.Remove(prod);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ProductViewModel> FindById(Guid id)
        {
            var prod = await _db.Product.FindAsync(id);
            ProductViewModel model = new ProductViewModel()
            {
                ProductId = prod.ProductId,
                Name = prod.Name,
                Description = prod.Description,
                Category = prod.Category,
                Price = prod.Price,
                Quantity = prod.Quantity,
                image = prod.Image,
            };

            return model;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var products = await _db.Product.ToListAsync();
            if (products == null)
            {
                return null;
            }
            List<ProductViewModel> productViewModels = products.Select(user => new ProductViewModel
            {
                ProductId = user.ProductId,
                Name = user.Name,
                Description = user.Description,
                Quantity = user.Quantity,
                Price = user.Price,
                Category = user.Category,
                image = user.Image
            }).ToList();
            return productViewModels;
        }

        public async Task<bool> Update(UpdateProductViewModel request)
        {
            try
            {
                var prod = await _db.Product.FindAsync(request.ProductId);
                if (prod == null)
                {
                    return false;
                }
                prod.Name = request.Name;
                prod.Description = request.Description;
                prod.Category = request.Category;
                prod.Price = request.Price;
                prod.Quantity = request.Quantity;
                if (request.Image is not null)
                {
                    prod.Image = request.Image;
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
