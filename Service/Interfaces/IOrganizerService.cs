using Repository.Models;
using Service.Models;
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
        Task DeleteOrganizerAsync(Guid organizerId);

        Task<ApiResponse<OrganizerDetailsDto>> GetOrganizeByIdAsync(string email);

        Task UpdateOrganizerAsync(UpdateOrganizerModel model, string email);

        Task<IEnumerable<Organizer>> GetAllOrganizesAsync();

        Task ChangeStatus(Guid organizerId, string status);

        Task<ApiResponse<Organizer>> CreateOrganizerAsync(OrganizerCreateModel createModel, string email);
    }
}
