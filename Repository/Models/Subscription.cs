using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Subscription
{
    public Guid SubscriptionId { get; set; }

    public Guid? UserId { get; set; }

    public string? Tier { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? AutoRenew { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
