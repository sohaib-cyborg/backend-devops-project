using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.ProductRepo
{
    public interface IProductRepo
    {
        Task<List<ProductViewModel>> GetProducts();
        Task<ProductViewModel> FindById(Guid id);
        Task<bool> Create(AddProductViewModel request);
        Task<bool> Update(UpdateProductViewModel request);
        Task<bool> Delete(Guid ProductId);
    }
}
