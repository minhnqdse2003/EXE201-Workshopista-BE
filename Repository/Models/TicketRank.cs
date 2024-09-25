using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class TicketRank
{
    public Guid TicketRankId { get; set; }

    public Guid? WorkshopId { get; set; }

    public string RankName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Capacity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Workshop? Workshop { get; set; }
}
