using Microsoft.EntityFrameworkCore;
using Repository.Consts;
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

        public async Task<IEnumerable<Order>> GetCompletedOrders()
        {
            var list = await _context.Orders.Where(o => o.PaymentStatus.Equals(PaymentStatus.Completed)).ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Order>> GetCompletedOrderOfOrganizer(Guid organizerId)
        {
            var workshopIdList = await _context.Workshops.Where(w => w.OrganizerId == organizerId).Select(w => w.WorkshopId).ToListAsync();
            var list = await _context.Orders.Where(o => o.PaymentStatus.Equals(PaymentStatus.Completed)).Include(o => o.OrderDetails).Where(o => workshopIdList.Contains(o.OrderDetails.FirstOrDefault().WorkshopId.Value)).ToListAsync();
            return list;
        }
    }
}
