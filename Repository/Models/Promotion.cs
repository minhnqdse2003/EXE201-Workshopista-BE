using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Promotion
{
    public Guid PromotionId { get; set; }

    public Guid? OrganizerId { get; set; }

    public Guid? WorkshopId { get; set; }

    public string? PromotionType { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Price { get; set; }

    public string? CurrencyCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Organizer? Organizer { get; set; }

    public virtual ICollection<PromotionTransaction> PromotionTransactions { get; set; } = new List<PromotionTransaction>();

    public virtual Workshop? Workshop { get; set; }
}
