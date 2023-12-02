using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.PaymentMethodRepo
{
    public interface IPaymentMethodRepo
    {
        Task<List<showPaymentMethodViewModel>> FindByUserId(string id);
        Task<bool> AddPayment(AddPaymentMethodViewModel request, string userId);
        Task<PaymentMethodViewModel> FindById(Guid id);
        Task<bool> Update(UpdateCustomerPaymentViewModel model, Guid PaymentMethodId);
        Task<bool> Delete(Guid id);
    }
}
