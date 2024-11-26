using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WorkshopRepository : GenericRepository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<Workshop> Get()
        {
            return _context.Workshops.AsQueryable();
        }

        public async Task<IEnumerable<Workshop>> GetWorkshopListByOrganizerId(Guid organizerId)
        {
            return await _context.Workshops.Where(w => w.OrganizerId.Equals(organizerId)).OrderByDescending(w => w.CreatedAt).ToListAsync ();
        }

    }
}
