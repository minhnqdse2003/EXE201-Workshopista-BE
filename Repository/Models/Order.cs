using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? ParticipantId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? CurrencyCode { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? Participant { get; set; }
}
