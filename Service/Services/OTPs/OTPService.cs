using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces.IOTP;
using Service.Models.OTPs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OTPs
{
    public class OTPService : IOTPService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OTPService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task VerifyOTPToActivateAccount(OTPVerifyRequestModel model)
        {
            User currentUser = await _unitOfWork.Users.GetUserByEmail(model.Email);

            if (currentUser == null)
            {
                throw new CustomException("Email bạn nhập không kết nối với tài khoản nào.");
            }

            var latestOTP = await _unitOfWork.OTPs.GetLatestOTP(currentUser.UserId);

            if (latestOTP != null)
            {
                if ((DateTime.Now - latestOTP.CreatedAt).Value.TotalMinutes > 30 || latestOTP.IsUsed == true)
                {
                    throw new CustomException("Mã OTP đã quá thời gian hoặc đã được sử dụng. Xin vui lòng nhập mã OTP mới nhất.");
                }

                if (latestOTP.Code.Equals(model.OTP))
                {
                    latestOTP.IsUsed = true;
                    currentUser.EmailVerified = true;
                }
                else
                {
                    throw new CustomException("Mã OTP không hợp lệ.");
                }

                await _unitOfWork.OTPs.Update(latestOTP);
                await _unitOfWork.Users.Update(currentUser);
            }
        }
    }
}
