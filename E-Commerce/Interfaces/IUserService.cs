using E_Commerce.DTOs;
using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IUserService
    {
        Task<User> Add(User user);
        User GetByCredentials(LoginDto Credentials);

        User Delete(User user);

        User Update(User user);

        User GetById(int id);

        bool isExisted(string username);
        IEnumerable<User> GetAll(); 
    }
}
