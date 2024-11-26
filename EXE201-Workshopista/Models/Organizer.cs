using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Organizer
{
    public Guid OrganizerId { get; set; }

    public Guid? UserId { get; set; }

    public string? OrganizationName { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? SocialLinks { get; set; }

    public bool? Verified { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual User? User { get; set; }

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
