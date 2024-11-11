using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WorkshopImageRepository : GenericRepository<WorkshopImage>, IWorkshopImageRepository
    {
        public WorkshopImageRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<WorkshopImage> GetWorkshopImages()
        {
            return _context.WorkshopImages.AsQueryable();
        }
    }
}
