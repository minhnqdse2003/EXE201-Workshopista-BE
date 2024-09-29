using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Repository.Repositories
{
    public class OTPRepository : GenericRepository<Otp>, IOTPRepository
    {
        public OTPRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<Otp?> GetLatestOTP(Guid userId)
        {
            var latestOTPList = await _context.Otps.Where(o => o.CreatedBy == userId).OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync();
            return latestOTPList;
        }

    }
}
