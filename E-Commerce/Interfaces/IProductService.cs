using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddAsync(Product product);    

        IEnumerable<Product> GetByCategory(string categoryName);

        IEnumerable<Product> GetAll();   

        Task<Product> GetById(int id); 

        Product Delete(Product product); 

        Product Update(Product product);
    }
}
