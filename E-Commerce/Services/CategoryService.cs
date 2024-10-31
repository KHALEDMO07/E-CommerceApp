using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);   

            return category;

        }

        public Category Delete(Category category)
        {
           _context.Categories.Remove(category);
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _context.Categories.Include(c => c.Products).ToList();
            return categories;
        }

        public Category GetByName(string name)
        {
            var category = _context.Categories.Include(c=>c.Products)
                .FirstOrDefault(c => c.Name == name);

            return category;
        }
    }
}
