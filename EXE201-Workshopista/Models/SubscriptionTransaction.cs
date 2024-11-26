using System;
using System.Collections.Generic;

namespace EXE201_Workshopista.Models;

public partial class SubscriptionTransaction
{
    public Guid SubscriptionTransactionId { get; set; }

    public Guid? SubscriptionId { get; set; }

    public Guid? TransactionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Subscription? Subscription { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
