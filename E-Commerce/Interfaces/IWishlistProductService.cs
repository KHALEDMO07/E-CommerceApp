using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IWishlistProductService
    {
        Task<WishlistProduct> Add(WishlistProduct product);

        WishlistProduct GetById(int id);

        WishlistProduct  Delete(WishlistProduct wishlistProduct);
    }
}
