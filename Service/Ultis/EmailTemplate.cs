using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Ultis
{
    public static class EmailTemplate
    {
        public static string VerifyEmailOTP(string fullname, string OTP)
        {
            var html = $@"<div style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                     <div style='border: 1px solid #e0e0e0; padding: 20px; max-width: 600px; margin: auto;'>
                         <p style='font-size: 16px;'>Dear <strong>{fullname}</strong>,</p>
                         <hr style='border: none; border-bottom: 1px solid #ccc; margin: 20px 0;'/>
                         <p style='font-size: 14px;'>
                            Your verification code (OTP) is: <strong style='font-size: 18px; color: #d9534f;'>{OTP}</strong><br/>
                            Please enter this code to verify your email address and activate your account.<br/>
                            Once verified, your account will be activated, and you can log in and use our services.<br/>
                            If you did not request email verification, please ignore this email or <a href='https://www.facebook.com/profile.php?id=61566296821667&mibextid=LQQJ4d' style='color: #0066cc;'>contact us</a> immediately.
                         </p>
                         <p style='font-size: 14px;'>This is an automated email, please do not reply.</p>
                         <p style='font-size: 14px;'>Best regards,<br/><strong>Workshopista</strong></p>
                     </div>
                 </div>";
            return html;
        }
    }
}

