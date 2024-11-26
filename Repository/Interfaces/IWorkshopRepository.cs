using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Workshop>
    {
        IQueryable<Workshop> Get();
        Task<IEnumerable<Workshop>> GetWorkshopListByOrganizerId(Guid organizerId);
    }
}
