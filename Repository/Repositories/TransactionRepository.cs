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

        public async Task<List<decimal>> GetAllTransaction()
        {
            return await _context.Transactions.Select(t => t.Amount.Value).ToListAsync();
        }

        public async Task<List<decimal>> GetInMonthTransaction()
        {
            var month = DateTime.Now.Month;
            return await _context.Transactions.Where(t => t.CreatedAt.Value.Month == month).Select(t => t.Amount.Value).ToListAsync();
        }

        public async Task<List<decimal>> GetInSevenDaysTransaction()
        {
            var current = DateTime.Now;
            var cutoffDate = current.AddDays(-7);
            return await _context.Transactions.Where(t => t.CreatedAt >= cutoffDate).Select(t => t.Amount.Value).ToListAsync();
        }
    }
}
