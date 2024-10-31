using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IWishlistService
    {
        Task<Wishlist> AddAsync(Wishlist wishlist);

        Wishlist GetById(int id);   

        Wishlist Delete(Wishlist wishlist); 

        Wishlist Update(Wishlist wishlist);
    }
}
