using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            return product;
        }

        public Product Delete(Product product)
        {
            _context.Products.Remove(product);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products.ToList();

            return products;
        }

        public IEnumerable<Product> GetByCategory(string categoryName)
        {
           var result = _context.Products.Include(p=>p.category).
                Where(p => p.category.Name == categoryName).OrderBy(p=>p.price).ToList();

            return result;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public Product Update(Product product)
        {
           _context.Update(product);
            return product;
        }
    }
}
