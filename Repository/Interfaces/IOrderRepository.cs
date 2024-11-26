using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> GetQuery();
        Task<IEnumerable<Order>> GetCompletedOrderOfOrganizer(Guid organizerId);
    }
}
