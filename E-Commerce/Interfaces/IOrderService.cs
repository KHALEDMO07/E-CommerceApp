using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IOrderService
    {
        Task<Order> AddAsync(Order order);

        IEnumerable<Order> GetAll(); 

        Order GetById(int id);

        Order Update(Order order);

        Order Delete(Order order);
    }
}
