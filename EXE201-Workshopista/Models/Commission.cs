using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Commission
{
    public Guid CommissionId { get; set; }

    public Guid? WorkshopId { get; set; }

    public decimal? CommissionRate { get; set; }

    public decimal? TotalCommission { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
