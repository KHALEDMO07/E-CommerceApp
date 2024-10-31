using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class OrderService(AppDbContext _context) : IOrderService
    {
        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public Order Delete(Order order)
        {
            _context.Orders.Remove(order);
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            var order = _context.Orders.ToList();

            return order;
        }

        public Order GetById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            return order;
        }

        public Order Update(Order order)
        {
            _context.Orders.Update(order);
            return order;
        }
    }
}
