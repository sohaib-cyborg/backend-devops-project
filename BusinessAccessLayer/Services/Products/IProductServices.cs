using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Products
{
    public interface IProductServices
    {
        Task<List<ProductViewModel>> GetProducts();
        Task<ProductViewModel> FindById(Guid id);
        Task<bool> Create(AddProductViewModel request);
        Task<bool> Update(UpdateProductViewModel request);
        Task<bool> Delete(Guid ProductId);
    }
}
