using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class PaymentMethod
{
    public Guid PaymentMethodId { get; set; }

    public string? MethodName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
