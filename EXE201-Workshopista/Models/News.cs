using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class News
{
    public Guid NewsId { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
