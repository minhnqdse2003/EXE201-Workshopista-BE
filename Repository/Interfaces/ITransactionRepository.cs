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
        Task<List<decimal>> GetAllTransaction();
        Task<List<decimal>> GetInMonthTransaction();
        Task<List<decimal>> GetInSevenDaysTransaction();
    }
}
