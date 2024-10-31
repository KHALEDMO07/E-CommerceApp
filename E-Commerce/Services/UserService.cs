using E_Commerce.Data;
using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);

            return user;
        }

        public User Delete(User user)
        {
            _context.Users.Remove(user);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.Include(u=>u.address).ToList();

            return users;
        }

        public User GetByCredentials(LoginDto Credentials)
        {
            var user = _context.Users.Include(u=>u.address)
                .FirstOrDefault(u => u.UserName == Credentials.UserName &&
            u.Password == Credentials.Password);

            return user;
        }

        public User GetById(int id)
        {
            var user = _context.Users.Include(u=>u.orders)
                .Include(u=>u.address)

                .FirstOrDefault(u => u.Id == id);

            return user;
        }

        public bool isExisted(string username)
        {
            bool res = _context.Users.Any(u => u.UserName == username);
            return res;
        }

        public User Update(User user)
        {
            _context.Update(user);
            return user;
        }
    }
}
