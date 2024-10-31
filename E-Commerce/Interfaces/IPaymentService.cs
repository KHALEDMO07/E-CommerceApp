using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> AddAsync(Payment payment);

        IEnumerable<Payment> GetAll(); 

        Payment GetById(int id);

        Payment Delete(Payment payment);  

        Payment Update(Payment payment);
    }
}
