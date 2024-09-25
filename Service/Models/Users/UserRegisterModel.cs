using Repository.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Users
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Please input your first name!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please input your last name!")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please input your email!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please input your password!")]
        [Length(6, 14, ErrorMessage = "Password must between 6-14 characters")]
        [PasswordComplexity]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please input your phone number!")]
        public string? PhoneNumber { get; set; }
    }
}
