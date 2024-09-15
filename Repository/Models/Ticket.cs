using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid? WorkshopId { get; set; }

    public Guid? ParticipantId { get; set; }

    public decimal? Price { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Status { get; set; }

    public string? QrCode { get; set; }

    public DateTime? PaymentTime { get; set; }

    public DateTime? BookingTime { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? Participant { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Workshop? Workshop { get; set; }
}
