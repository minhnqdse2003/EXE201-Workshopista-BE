using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class PromotionTransaction
{
    public Guid PromotionTransactionId { get; set; }

    public Guid? TransactionId { get; set; }

    public Guid? PromotionId { get; set; }

    public Guid? WorkshopId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public string? PromotionType { get; set; }

    public virtual Promotion? Promotion { get; set; }

    public virtual Transaction? Transaction { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
