using Service.Models.OTPs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.IOTP
{
    public interface IOTPService
    {
        Task VerifyOTPToActivateAccount(OTPVerifyRequestModel model);
    }
}
