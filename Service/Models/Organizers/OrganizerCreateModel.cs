using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Organizers
{
    public class OrganizerCreateModel
    { 
        public string? OrganizationName { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? SocialLinks { get; set; }

        public bool? Verified { get; set; }
    }
}
