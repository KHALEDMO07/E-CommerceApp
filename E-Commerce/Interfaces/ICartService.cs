using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface ICartService
    {
        Task<Cart> AddAsync(Cart cart);
        Cart GetById(int  id);  

        Cart Delete(Cart cart);

        Cart Update(Cart cart); 

    }
}
