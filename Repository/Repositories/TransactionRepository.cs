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

        public async Task<IEnumerable<Transaction>> GetAllTransaction()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetInMonthTransaction()
        {
            var month = DateTime.Now.Month;
            return await _context.Transactions.Where(t => t.CreatedAt.Value.Month == month).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetInSevenDaysTransaction()
        {
            var current = DateTime.Now;
            return await _context.Transactions.Where(t => current.Subtract(t.CreatedAt.Value).TotalDays <= 7).ToListAsync();
        }
    }
}
