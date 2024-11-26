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
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<Transaction> GetQuery() => _context.Transactions.AsQueryable();

        public async Task<List<Transaction>> GetAllTransaction()
        {
            return await _context.Transactions.Where(t => t.PaymentMethodId != null).ToListAsync();
        }
    }
}
