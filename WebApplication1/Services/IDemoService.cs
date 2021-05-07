using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDemoService
    {
        Task<bool> DeleteUser(int id);
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> PostUser(User user);
        Task<bool> PutUser(int id, User user);
        Task<IEnumerable<User>> SearchUser(string name);
    }
}