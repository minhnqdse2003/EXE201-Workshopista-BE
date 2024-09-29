using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOTPRepository : IGenericRepository<Otp>
    {
        Task<Otp?> GetLatestOTP(Guid userId);
    }
}
