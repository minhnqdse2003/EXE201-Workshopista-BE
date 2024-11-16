using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Organizers
{
    public class OrganizerDetailsDto
    {
        public string OrganizationName { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string ContactPhone { get; set; } = null!;

        public string WebsiteUrl { get; set; } = null!;

        public string SocialLinks { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
