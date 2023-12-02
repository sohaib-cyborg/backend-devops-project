using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.DAL.PaymentMethodRepo;
using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.PaymentMethods
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepo _paymentMethod;

        public PaymentMethodService(IPaymentMethodRepo paymentMethod)
        {
            this._paymentMethod = paymentMethod;
        }

        public async Task<bool> AddPayment(AddPaymentMethodViewModel request,string userId)
        {
           if(await _paymentMethod.AddPayment(request, userId))
            {
                return true;
            }
           return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            if(await _paymentMethod.Delete(id))
            {
                return true;
            }
            return false;
        }

        public async Task<PaymentMethodViewModel> FindById(Guid id)
        {
            var payments = await _paymentMethod.FindById(id);
            return payments;
        }

        public async Task<List<showPaymentMethodViewModel>> FindByUserId(string id)
        {
            var payments = await _paymentMethod.FindByUserId(id);
            return payments;
        }

        public async Task<bool> Update(UpdateCustomerPaymentViewModel model, Guid PaymentMethodId)
        {
            if(await _paymentMethod.Update(model, PaymentMethodId))
            {
                return true;
            }
            return false;
        }
    }
}
