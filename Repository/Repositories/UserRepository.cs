using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.Include(x => x.Organizers).FirstOrDefaultAsync(x => x.Email == userName);
        }

        public async Task<User?> GetUserByRefreshToken(string? token)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
            return result;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();
        }
    }
}
