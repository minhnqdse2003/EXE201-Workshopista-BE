﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.OTPs
{
    public class OTPVerifyRequestModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Please input your email")]

        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Please input your OTP")]
        public string OTP { get; set; } = null!;
    }
}