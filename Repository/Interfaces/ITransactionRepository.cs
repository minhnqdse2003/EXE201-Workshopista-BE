using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        IQueryable<Transaction> GetQuery();
        Task<IEnumerable<Transaction>> GetAllTransaction();
        Task<IEnumerable<Transaction>> GetInMonthTransaction();
        Task<IEnumerable<Transaction>> GetInSevenDaysTransaction();
    }
}
