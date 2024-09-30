using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CommissionTransactionRepository : GenericRepository<CommissionTransaction>, ICommissionTransactionRepository
    {
        public CommissionTransactionRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }
    }
}
