using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Ultis
{
    public static class OTPGeneration
    {
        public static string CreateNewOTPCode()
        {
            return new Random().Next(0, 999999).ToString("D6");
        }
    }
}
