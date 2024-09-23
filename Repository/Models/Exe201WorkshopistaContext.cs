﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Extensions;

namespace Repository.Models;

public partial class Exe201WorkshopistaContext : DbContext
{
    public Exe201WorkshopistaContext()
    {
    }

    public Exe201WorkshopistaContext(DbContextOptions<Exe201WorkshopistaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Commission> Commissions { get; set; }

    public virtual DbSet<EventAnalytic> EventAnalytics { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    public virtual DbSet<WorkshopImage> WorkshopImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer(GetConnectionString());
    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DBUtilsConnectionString"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__9E2397E0775D0C7B");

            entity.ToTable("AuditLog");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.Entity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("entity");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.PerformedBy).HasColumnName("performed_by");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B4999D4519");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Slug, "UQ__Category__32DD1E4CA9B28410").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Category__72E12F1BF3C908B9").IsUnique();

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Commission>(entity =>
        {
            entity.HasKey(e => e.CommissionId).HasName("PK__Commissi__D19D7CC994FB9C82");

            entity.ToTable("Commission");

            entity.Property(e => e.CommissionId)
                .ValueGeneratedNever()
                .HasColumnName("commission_id");
            entity.Property(e => e.CommissionRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("commission_rate");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TotalCommission)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_commission");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Commissions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_Commission_Workshop");
        });

        modelBuilder.Entity<EventAnalytic>(entity =>
        {
            entity.HasKey(e => e.AnalyticsId).HasName("PK__EventAna__D5DC3DE1137F8C0D");

            entity.Property(e => e.AnalyticsId)
                .ValueGeneratedNever()
                .HasColumnName("analytics_id");
            entity.Property(e => e.AverageRating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("average_rating");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TotalRevenue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_revenue");
            entity.Property(e => e.TotalReviews).HasColumnName("total_reviews");
            entity.Property(e => e.TotalTicketsSold).HasColumnName("total_tickets_sold");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.EventAnalytics)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_EventAnalytics_Workshop");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD8F7E3AF43");

            entity.HasIndex(e => e.Slug, "UQ__News__32DD1E4C5A8B7C4F").IsUnique();

            entity.Property(e => e.NewsId)
                .ValueGeneratedNever()
                .HasColumnName("news_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__465962298BD6BFF4");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.PaymentTime)
                .HasColumnType("datetime")
                .HasColumnName("payment_time");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Participant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK__OrderDet__F6FB5AE44C7C30C5");

            entity.Property(e => e.OrderDetailsId)
                .ValueGeneratedNever()
                .HasColumnName("order_details_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_Order");

            entity.HasOne(d => d.Ticket).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_OrderDetails_Ticket");

            entity.HasOne(d => d.Workshop).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_OrderDetails_Workshop");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.OrganizerId).HasName("PK__Organize__063470141F69041A");

            entity.ToTable("Organizer");

            entity.Property(e => e.OrganizerId)
                .ValueGeneratedNever()
                .HasColumnName("organizer_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_phone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("organization_name");
            entity.Property(e => e.SocialLinks)
                .HasColumnType("text")
                .HasColumnName("social_links");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Verified)
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website_url");

            entity.HasOne(d => d.User).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Organizer_User");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__8A3EA9EBA7FDCED2");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId)
                .ValueGeneratedNever()
                .HasColumnName("payment_method_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.MethodName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("method_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556B2C8CD7C8");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId)
                .ValueGeneratedNever()
                .HasColumnName("promotion_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PromotionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("promotion_type");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK_Promotion_Organizer");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_Promotion_Workshop");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__60883D90FB2B51AA");

            entity.ToTable("Review");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("review_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Participant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Review_Participant");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_Review_Workshop");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC1425E97CF");

            entity.ToTable("Subscription");

            entity.Property(e => e.SubscriptionId)
                .ValueGeneratedNever()
                .HasColumnName("subscription_id");
            entity.Property(e => e.AutoRenew).HasColumnName("auto_renew");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Tier)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tier");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Subscription_User");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__D596F96BCA20FFB1");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_id");
            entity.Property(e => e.BookingTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("booking_time");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.PaymentTime)
                .HasColumnType("datetime")
                .HasColumnName("payment_time");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.QrCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("qr_code");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Participant).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Ticket_Participant");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_Ticket_Workshop");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__85C600AFBC29D7EE");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.TransactionReference, "UQ__Transact__F0DAF2E82F8DC13B").IsUnique();

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.TransactionReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transaction_reference");
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transaction_status");
            entity.Property(e => e.TransactionTime)
                .HasColumnType("datetime")
                .HasColumnName("transaction_time");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Participant).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Transaction_Participant");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_Transaction_PaymentMethod");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_Transaction_Ticket");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F02E16F77");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164168C33A0").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasDefaultValue(false)
                .HasColumnName("email_verified");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhoneVerified)
                .HasDefaultValue(false)
                .HasColumnName("phone_verified");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_image_url");
            entity.Property(e => e.RefreshToken)
                .HasColumnType("text")
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("refresh_token_expiry_time");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.WorkshopId).HasName("PK__Workshop__EA6B0559AAAD1465");

            entity.ToTable("Workshop");

            entity.HasIndex(e => e.Slug, "UQ__Workshop__32DD1E4C1ED9DC10").IsUnique();

            entity.Property(e => e.WorkshopId)
                .ValueGeneratedNever()
                .HasColumnName("workshop_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("currency_code");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitude");
            entity.Property(e => e.LocationAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_address");
            entity.Property(e => e.LocationCity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_city");
            entity.Property(e => e.LocationDistrict)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_district");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitude");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Category).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Workshop_Category");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK_Workshop_Organizer");
        });

        modelBuilder.Entity<WorkshopImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Workshop__DC9AC95561A32F46");

            entity.ToTable("WorkshopImage");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsPrimary)
                .HasDefaultValue(false)
                .HasColumnName("is_primary");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.WorkshopImages)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_WorkshopImage_Workshop");
        });
        ModelBuilderExtensions.SeedUsers(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
