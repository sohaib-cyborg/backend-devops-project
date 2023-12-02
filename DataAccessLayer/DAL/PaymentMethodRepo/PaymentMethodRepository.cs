using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.PaymentMethodRepo
{
    public class PaymentMethodRepository :IPaymentMethodRepo
    {
        private readonly AuthAPIDbContext _db;

        public PaymentMethodRepository()
        {
            this._db = SingletonDbContext.GetDbContext();
        }
        public async Task<bool> AddPayment(AddPaymentMethodViewModel request, string userId)
        {
            try
            {
                PaymentMethod paymentMethod = new PaymentMethod()
                {
                    PaymentMethodId = Guid.NewGuid(),
                    UserId = userId,
                    CardNumber = request.CardNumber,
                    Provider = request.Provider
                };
                await _db.PaymentMethod.AddAsync(paymentMethod);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var payment = await _db.PaymentMethod.FindAsync(id);
                _db.PaymentMethod.Remove(payment);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<PaymentMethodViewModel> FindById(Guid id)
        {
            try
            {
                var payment = await _db.PaymentMethod.FindAsync(id);
                PaymentMethodViewModel model = new PaymentMethodViewModel()
                {
                    PaymentMethodId = payment.PaymentMethodId,
                    Provider = payment.Provider,
                    CardNumber = payment.CardNumber,
                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<showPaymentMethodViewModel>> FindByUserId(string id)
        {
            try
            {
                var paymentMethods = await _db.PaymentMethod.Where(x => x.UserId == id).ToListAsync();
                List<showPaymentMethodViewModel> result = new List<showPaymentMethodViewModel>(paymentMethods.Count);
                result.AddRange(paymentMethods.Select(x => new showPaymentMethodViewModel
                {
                    UserId = x.UserId,
                    PaymentMethodId = x.PaymentMethodId,
                    Provider = x.Provider
                }));
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(UpdateCustomerPaymentViewModel model, Guid PaymentMethodId)
        {
            try
            {
                var payment = await _db.PaymentMethod.FindAsync(PaymentMethodId);
                if (payment == null)
                {
                    return false;
                }
                payment.Provider = model.Provider;
                payment.CardNumber = model.CardNumber;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
