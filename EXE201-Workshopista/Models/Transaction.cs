using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserId { get; set; }

    public string? TransactionType { get; set; }

    public decimal? Amount { get; set; }

    public string? CurrencyCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommissionTransaction> CommissionTransactions { get; set; } = new List<CommissionTransaction>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<PromotionTransaction> PromotionTransactions { get; set; } = new List<PromotionTransaction>();

    public virtual ICollection<SubscriptionTransaction> SubscriptionTransactions { get; set; } = new List<SubscriptionTransaction>();

    public virtual User? User { get; set; }
}
