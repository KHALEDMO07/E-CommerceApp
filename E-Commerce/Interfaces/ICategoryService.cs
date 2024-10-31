using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface ICategoryService
    {
        Task<Category>AddAsync(Category category);

        IEnumerable<Category> GetAll();

      
        Category GetByName(string name); 

        Category Delete (Category category);
    }
}
