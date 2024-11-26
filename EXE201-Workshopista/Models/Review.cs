using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Review
{
    public Guid ReviewId { get; set; }

    public Guid? WorkshopId { get; set; }

    public Guid? ParticipantId { get; set; }

    public short? Rating { get; set; }

    public string? Comment { get; set; }

    public string? ReviewStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Participant { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
