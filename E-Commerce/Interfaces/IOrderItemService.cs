using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItem> AddAsync(OrderItem orderItem);  
    }
}
