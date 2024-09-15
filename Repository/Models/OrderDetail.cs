using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class OrderDetail
{
    public Guid OrderDetailsId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? WorkshopId { get; set; }

    public Guid? TicketId { get; set; }

    public decimal? Price { get; set; }

    public string? CurrencyCode { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
