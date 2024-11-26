using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
