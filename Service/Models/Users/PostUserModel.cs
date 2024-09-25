using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Users
{
    public class PostUserModel
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Role { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool? EmailVerified { get; set; }

        public bool? PhoneVerified { get; set; }
    }
}
