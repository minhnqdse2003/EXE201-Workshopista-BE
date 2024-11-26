using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid? WorkshopId { get; set; }

    public Guid? OrderDetailId { get; set; }

    public Guid? TicketRankId { get; set; }

    public decimal? Price { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Status { get; set; }

    public string? QrCode { get; set; }

    public DateTime? PaymentTime { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual TicketRank? TicketRank { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
