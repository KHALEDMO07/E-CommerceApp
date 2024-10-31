using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class WishlistService(AppDbContext _context) : IWishlistService
    {
        
        public async Task<Wishlist> AddAsync(Wishlist wishlist)
        {
           await  _context.Wishlists.AddAsync(wishlist);

            return wishlist;
        }

        public Wishlist Delete(Wishlist wishlist)
        {
            _context.Wishlists.Remove(wishlist);
            return wishlist;    
        }

        public Wishlist GetById(int id)
        {
           var wishlist  = _context.Wishlists.Include(w=>w.WishlistProducts)
                .FirstOrDefault(w => w.Id == id);

            return wishlist;
        }

        public Wishlist Update(Wishlist wishlist)
        {
            _context.Update(wishlist);
            return wishlist;    
        }
    }
}
