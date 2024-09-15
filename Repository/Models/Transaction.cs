using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid? TicketId { get; set; }

    public Guid? ParticipantId { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public string? TransactionStatus { get; set; }

    public decimal? Amount { get; set; }

    public string? CurrencyCode { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime? TransactionTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Participant { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
