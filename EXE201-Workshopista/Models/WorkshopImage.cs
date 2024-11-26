using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class WorkshopImage
{
    public Guid ImageId { get; set; }

    public Guid? WorkshopId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsPrimary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
