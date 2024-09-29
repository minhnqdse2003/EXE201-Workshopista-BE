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
    public class WorkshopRepository : GenericRepository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<Workshop> Get()
        {
            return _context.Workshops.AsQueryable();
        }
    }
}
