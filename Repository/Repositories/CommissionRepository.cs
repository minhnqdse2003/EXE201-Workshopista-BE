using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CommissionRepository : GenericRepository<Commission>, ICommissionRepository
    {
        public CommissionRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }
    }
}
