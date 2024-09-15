﻿using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Workshop
{
    public Guid WorkshopId { get; set; }

    public Guid? OrganizerId { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public Guid? CategoryId { get; set; }

    public string? LocationCity { get; set; }

    public string? LocationDistrict { get; set; }

    public string? LocationAddress { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public decimal? Price { get; set; }

    public string? CurrencyCode { get; set; }

    public int? Capacity { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<EventAnalytic> EventAnalytics { get; set; } = new List<EventAnalytic>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Organizer? Organizer { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
