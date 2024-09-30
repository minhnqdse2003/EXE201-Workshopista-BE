using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SubscriptionTransactionRepository : GenericRepository<SubscriptionTransaction>, ISubscriptionTransactionRepository
    {
        public SubscriptionTransactionRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<SubscriptionTransaction> GetQuery() => _context.SubscriptionTransactions.AsQueryable();
    }
}
