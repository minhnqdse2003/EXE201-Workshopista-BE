using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrganizerRepository : GenericRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<Organizer?> GetOrganizerByEmail(string email)
        {
            return await _context.Organizers.Where(u => u.ContactEmail.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Organizer>> GetAllOrganizersAsync()
        {
            return await _context.Organizers.ToListAsync();
        }

        public async Task<Organizer> GetOrganizerByIdAsync(Guid organizerId)
        {
            return await _context.Organizers.FindAsync(organizerId);
        }

        public async Task CreateOrganizeAsync(Organizer organizer)
        {
            await _context.Organizers.AddAsync(organizer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrganizerAsync(Organizer organizer)
        {
            _context.Organizers.Update(organizer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrganizerAsync(Guid organizerId)
        {
            var organizer = await _context.Users.FindAsync(organizerId);
            if (organizer != null)
            {
                _context.Users.Remove(organizer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == userName);
        }

        public Task<User?> GetOrganizerByRefreshToken(string token)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
        }

        public Task<Organizer> GetOrganizeByIdAsync(Guid organizerId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetOrganizerByOrganizationNameAsync(string organizationName)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrganizerAsync(Organizer organizer)
        {
            throw new NotImplementedException();
        }
    }
}
