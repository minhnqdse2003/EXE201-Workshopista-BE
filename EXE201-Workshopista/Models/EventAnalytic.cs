using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class EventAnalytic
{
    public Guid AnalyticsId { get; set; }

    public Guid? WorkshopId { get; set; }

    public int? TotalTicketsSold { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? AverageRating { get; set; }

    public int? TotalReviews { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
