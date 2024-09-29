using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Otp
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsUsed { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
