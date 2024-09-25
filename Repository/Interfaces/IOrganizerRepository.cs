using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrganizerRepository : IGenericRepository<Organizer>
    {
        Task<Organizer?> GetOrganizerByEmail(string email);

        Task<IEnumerable<Organizer>> GetAllOrganizersAsync();

        Task<Organizer> GetOrganizerByIdAsync(Guid organizerId);

        Task<User?> GetOrganizerByOrganizationNameAsync(string organizationName);

        Task<User?> GetOrganizerByRefreshToken(string token);

        Task CreateOrganizerAsync(Organizer organizer);

        Task UpdateOrganizerAsync(Organizer organizer);

        Task DeleteOrganizerAsync(Guid organizerId);

    }
}
