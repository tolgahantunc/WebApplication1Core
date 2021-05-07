using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DemoService : IDemoService
    {
        private readonly DemoDBContext _context;

        public DemoService(DemoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.User.FindAsync(id);
        }
        public async Task<IEnumerable<User>> SearchUser(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            List<User> lsUser = null;
            lsUser = await _context.User.Where(x => x.Name.Equals(name)).ToListAsync();
            return lsUser;
        }

        public async Task<bool> PutUser(int id, User user)
        {
            if (user == null || id != user.UserId)            
                return false;            

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))                
                    return false;              
                else                
                    throw;                
            }

            return true;
        }

        public async Task<bool> PostUser(User user)
        {
            if (user == null)
                return false;

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;            

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
