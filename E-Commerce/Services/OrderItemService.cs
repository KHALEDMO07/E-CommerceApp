using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class OrderItemService(AppDbContext _context) : IOrderItemService
    {
        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);  
            return orderItem;
        }
    }
}
