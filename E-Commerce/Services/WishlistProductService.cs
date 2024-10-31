using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class WishlistProductService(AppDbContext _context) : IWishlistProductService
    {
        public async Task<WishlistProduct> Add(WishlistProduct product)
        {
            await _context.WishlistProduct.AddAsync(product);

            return product;
        }

        public WishlistProduct Delete(WishlistProduct wishlistProduct)
        {
            _context.Remove(wishlistProduct);
            return wishlistProduct;
        }

        public WishlistProduct GetById(int id)
        {
            var product = _context.WishlistProduct.Include(x=>x.whishlist)
                .FirstOrDefault(x=> x.Id == id);

            return product;
        }
    }
}
