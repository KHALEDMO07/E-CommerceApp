using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class CartService(AppDbContext _context) : ICartService
    {
        public async Task<Cart> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return cart;    
        }

        public Cart Delete(Cart cart)
        {
            _context.Carts.Remove(cart);
            return cart;
        }

        public Cart GetById(int id)
        {
           return _context.Carts.Include(x => x.CartProducts)
               . FirstOrDefault(c => c.Id == id);
        }

        public Cart Update(Cart cart)
        {
            _context.Carts.Update(cart);
            return cart;
        }
    }
}
