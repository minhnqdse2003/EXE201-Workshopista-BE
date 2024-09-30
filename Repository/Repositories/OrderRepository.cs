using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<Order> GetQuery()
        {
            return _context.Orders.AsQueryable();
        }
    }
}
