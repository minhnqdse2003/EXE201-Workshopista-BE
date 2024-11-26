using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class AuditLog
{
    public Guid LogId { get; set; }

    public string? Entity { get; set; }

    public Guid? EntityId { get; set; }

    public string? Action { get; set; }

    public Guid? PerformedBy { get; set; }

    public DateTime? Timestamp { get; set; }
}
