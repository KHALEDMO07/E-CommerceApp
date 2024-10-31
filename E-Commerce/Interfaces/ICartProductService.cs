using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface ICartProductService
    {
        Task<CartProduct> AddAsync(CartProduct product);

        CartProduct GetById(int id);

        CartProduct Delete(CartProduct product);
    }
}
