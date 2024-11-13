using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Organizers
{
    public class UpdateOrganizerModel
    {
        [Required(ErrorMessage ="Please input organization's name")]
        public required string OrganizationName { get; set; }
        [Required(ErrorMessage = "Please input organization's mail")]
        public required string ContactEmail { get; set; }
        [Required(ErrorMessage = "Please input organization's contact phone")]
        public required string ContactPhone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? SocialLinks { get; set; }
    }
}
