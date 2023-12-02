using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.PaymentMethods
{
    public interface IPaymentMethodService
    {
        Task<List<showPaymentMethodViewModel>> FindByUserId(string id);
        Task<bool> AddPayment(AddPaymentMethodViewModel request,string userId);
        Task<PaymentMethodViewModel> FindById(Guid id);
        Task<bool> Update(UpdateCustomerPaymentViewModel model,Guid PaymentMethodId);
        Task<bool> Delete(Guid id);
    }
}
