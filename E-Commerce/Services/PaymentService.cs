using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace E_Commerce.Services
{
    public class PaymentService(AppDbContext _context) : IPaymentService
    {
        public async  Task<Payment> AddAsync(Payment payment)
        {
            await _context.AddAsync(payment);
            return payment;
        }

        public Payment Delete(Payment payment)
        {
           _context.Remove(payment);

            return payment; 
        }

        public IEnumerable<Payment> GetAll()
        {
           var payments = _context.Payments.ToList();

            return payments;
        }

        public Payment GetById(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            return payment;
        }

        public Payment Update(Payment payment)
        {
            _context.Update(payment);
            return payment;
        }
    }
}
