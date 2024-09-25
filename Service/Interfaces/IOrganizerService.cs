using Repository.Models;
using Service.Models.Organizers;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrganizerService
    {
        Task RegisterOrganizerAccount(OrganizerRegisterModel model);
        Task DeleteOrganizerAsync(Guid organizerId);
        Task<Organizer> GetOrganizeByIdAsync(Guid organizerId);
        Task UpdateOrganizerAsync(UpdateOrganizerModel model, Guid id);
        Task<IEnumerable<Organizer>> GetAllOrganizesAsync();
    }
}
