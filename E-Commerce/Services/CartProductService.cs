using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class CartProductService(AppDbContext _context) : ICartProductService
    {
        public async Task<CartProduct> AddAsync(CartProduct product)
        {
            await _context.CartProducts.AddAsync(product);
            return product;
        }

        public CartProduct Delete(CartProduct product)
        {
            _context.CartProducts.Remove(product);
            return product;
        }

        public CartProduct GetById(int id)
        {
            var product = _context.CartProducts.Include(x => x.cart)
                .FirstOrDefault(x => x.Id == id);

            return product;
        }
    }
}
