using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<OrderDetail> GetQuery() => _context.OrderDetails.AsQueryable();
    }
}
