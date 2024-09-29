using System;
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

    public virtual DbSet<CommissionTransaction> CommissionTransactions { get; set; }

    public virtual DbSet<EventAnalytic> EventAnalytics { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PromotionTransaction> PromotionTransactions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionTransaction> SubscriptionTransactions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketRank> TicketRanks { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    public virtual DbSet<WorkshopImage> WorkshopImages { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DBUtilsConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__9E2397E0273670BF");

            entity.ToTable("AuditLog");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Entity)
                .HasMaxLength(50)
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
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B420B124AB");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Slug, "UQ__Category__32DD1E4C475F73D5")
                .IsUnique()
                .HasFilter("([slug] IS NOT NULL)");

            entity.HasIndex(e => e.Name, "UQ__Category__72E12F1B8F3F9A5C")
                .IsUnique()
                .HasFilter("([name] IS NOT NULL)");

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
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Commission>(entity =>
        {

            entity.HasKey(e => e.CommissionId).HasName("PK__Commissi__D19D7CC90A4035D4");


            entity.ToTable("Commission");

            entity.HasIndex(e => e.WorkshopId, "IX_Commission_workshop_id");

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
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_commission");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Commissions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__Commissio__works__2645B050");
        });

        modelBuilder.Entity<CommissionTransaction>(entity =>
        {
            entity.HasKey(e => e.CommissionTransactionId).HasName("PK__Commissi__32A99DDF3CA1F70E");

            entity.ToTable("CommissionTransaction");

            entity.HasIndex(e => e.TransactionId, "IX_CommissionTransaction_transaction_id");

            entity.HasIndex(e => e.WorkshopId, "IX_CommissionTransaction_workshop_id");

            entity.Property(e => e.CommissionTransactionId)
                .ValueGeneratedNever()
                .HasColumnName("commission_transaction_id");
            entity.Property(e => e.CommissionRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("commission_rate");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Transaction).WithMany(p => p.CommissionTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK_CommissionTransaction_Transaction");

            entity.HasOne(d => d.Workshop).WithMany(p => p.CommissionTransactions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_CommissionTransaction_Workshop");
        });

        modelBuilder.Entity<EventAnalytic>(entity =>
        {
            entity.HasKey(e => e.AnalyticsId).HasName("PK__EventAna__D5DC3DE1912B45AA");

            entity.HasIndex(e => e.WorkshopId, "IX_EventAnalytics_workshop_id");

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
                .HasColumnType("decimal(18, 2)")
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
                .HasConstraintName("FK__EventAnal__works__22751F6C");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD8D61AD16C");

            entity.HasIndex(e => e.Slug, "UQ__News__32DD1E4CEEFB436D")
                .IsUnique()
                .HasFilter("([slug] IS NOT NULL)");

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
                .HasColumnName("image_url");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__46596229D5600F2F");

            entity.ToTable("Order");

            entity.HasIndex(e => e.ParticipantId, "IX_Order_participant_id");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasColumnName("payment_status");
            entity.Property(e => e.PaymentTime)
                .HasColumnType("datetime")
                .HasColumnName("payment_time");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Participant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK__Order__participa__1AD3FDA4");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK__OrderDet__F6FB5AE4C762AF40");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_order_id");

            entity.HasIndex(e => e.TicketId, "IX_OrderDetails_ticket_id");

            entity.HasIndex(e => e.WorkshopId, "IX_OrderDetails_workshop_id");

            entity.Property(e => e.OrderDetailsId)
                .ValueGeneratedNever()
                .HasColumnName("order_details_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__order__1BC821DD");

            entity.HasOne(d => d.Ticket).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__OrderDeta__ticke__1DB06A4F");

            entity.HasOne(d => d.Workshop).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__OrderDeta__works__1CBC4616");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.OrganizerId).HasName("PK__Organize__06347014B8E42884");

            entity.ToTable("Organizer");

            entity.HasIndex(e => e.UserId, "IX_Organizer_user_id");

            entity.Property(e => e.OrganizerId)
                .ValueGeneratedNever()
                .HasColumnName("organizer_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
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
                .HasColumnName("website_url");

            entity.HasOne(d => d.User).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Organizer__user___0D7A0286");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.ToTable("OTP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Otps)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_OTP_User");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__8A3EA9EBF19ACE4A");

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
                .HasColumnName("method_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556B36D75614");

            entity.ToTable("Promotion");

            entity.HasIndex(e => e.OrganizerId, "IX_Promotion_organizer_id");

            entity.HasIndex(e => e.WorkshopId, "IX_Promotion_workshop_id");

            entity.Property(e => e.PromotionId)
                .ValueGeneratedNever()
                .HasColumnName("promotion_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PromotionType)
                .HasMaxLength(50)
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
                .HasConstraintName("FK__Promotion__organ__245D67DE");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__Promotion__works__25518C17");
        });

        modelBuilder.Entity<PromotionTransaction>(entity =>
        {
            entity.HasKey(e => e.PromotionTransactionId).HasName("PK__Promotio__A5C4F6151910D125");

            entity.ToTable("PromotionTransaction");

            entity.HasIndex(e => e.PromotionId, "IX_PromotionTransaction_promotion_id");

            entity.HasIndex(e => e.TransactionId, "IX_PromotionTransaction_transaction_id");

            entity.HasIndex(e => e.WorkshopId, "IX_PromotionTransaction_workshop_id");

            entity.Property(e => e.PromotionTransactionId)
                .ValueGeneratedNever()
                .HasColumnName("promotion_transaction_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Promotion).WithMany(p => p.PromotionTransactions)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_PromotionTransaction_Promotion");

            entity.HasOne(d => d.Transaction).WithMany(p => p.PromotionTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK_PromotionTransaction_Transaction");

            entity.HasOne(d => d.Workshop).WithMany(p => p.PromotionTransactions)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_PromotionTransaction_Workshop");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__60883D90378C5259");

            entity.ToTable("Review");

            entity.HasIndex(e => e.ParticipantId, "IX_Review_participant_id");

            entity.HasIndex(e => e.WorkshopId, "IX_Review_workshop_id");

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
                .HasColumnName("review_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Participant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Review_User");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__Review__workshop__208CD6FA");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC1993FA16B");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.UserId, "IX_Subscription_user_id");

            entity.Property(e => e.SubscriptionId)
                .ValueGeneratedNever()
                .HasColumnName("subscription_id");
            entity.Property(e => e.AutoRenew)
                .HasDefaultValue(true)
                .HasColumnName("auto_renew");
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
                .HasColumnName("tier");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Subscript__user___236943A5");
        });

        modelBuilder.Entity<SubscriptionTransaction>(entity =>
        {
            entity.HasKey(e => e.SubscriptionTransactionId).HasName("PK__Subscrip__762A0D4C91ACD385");

            entity.ToTable("SubscriptionTransaction");

            entity.HasIndex(e => e.SubscriptionId, "IX_SubscriptionTransaction_subscription_id");

            entity.HasIndex(e => e.TransactionId, "IX_SubscriptionTransaction_transaction_id");

            entity.Property(e => e.SubscriptionTransactionId)
                .ValueGeneratedNever()
                .HasColumnName("subscription_transaction_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionTransactions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK_SubscriptionTransaction_Subscription");

            entity.HasOne(d => d.Transaction).WithMany(p => p.SubscriptionTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK_SubscriptionTransaction_Transaction");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__D596F96B7099B05A");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.OrderDetailId, "IX_Ticket_order_detail_id");

            entity.HasIndex(e => e.TicketRankId, "IX_Ticket_ticket_rank_id");

            entity.HasIndex(e => e.WorkshopId, "IX_Ticket_workshop_id");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_id");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.PaymentTime)
                .HasColumnType("datetime")
                .HasColumnName("payment_time");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.QrCode)
                .HasMaxLength(255)
                .HasColumnName("qr_code");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TicketRankId).HasColumnName("ticket_rank_id");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK_Ticket_OrderDetails");

            entity.HasOne(d => d.TicketRank).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketRankId)
                .HasConstraintName("FK_Ticket_TicketRank");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__Ticket__workshop__114A936A");
        });

        modelBuilder.Entity<TicketRank>(entity =>
        {
            entity.HasKey(e => e.TicketRankId).HasName("PK__TicketRa__1B8160B1D43B79A1");

            entity.ToTable("TicketRank");

            entity.HasIndex(e => e.WorkshopId, "IX_TicketRank_workshop_id");

            entity.Property(e => e.TicketRankId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_rank_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RankName)
                .HasMaxLength(255)
                .HasColumnName("rank_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.TicketRanks)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_TicketRank_Workshop");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__85C600AF4A720ABE");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.PaymentMethodId, "IX_Transaction_payment_method_id");

            entity.HasIndex(e => e.UserId, "IX_Transaction_user_id");

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
                .HasColumnName("currency_code");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Transacti__payme__14270015");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Transaction_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F4336E3EF");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E616457DE1028")
                .IsUnique()
                .HasFilter("([email] IS NOT NULL)");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasDefaultValue(false)
                .HasColumnName("email_verified");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhoneVerified)
                .HasDefaultValue(false)
                .HasColumnName("phone_verified");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(255)
                .HasColumnName("profile_image_url");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("refresh_token_expiry_time");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.WorkshopId).HasName("PK__Workshop__EA6B0559D8B60A03");

            entity.ToTable("Workshop");

            entity.HasIndex(e => e.CategoryId, "IX_Workshop_category_id");

            entity.HasIndex(e => e.OrganizerId, "IX_Workshop_organizer_id");

            entity.HasIndex(e => e.Slug, "UQ__Workshop__32DD1E4CC1ABD095")
                .IsUnique()
                .HasFilter("([slug] IS NOT NULL)");

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
                .HasColumnName("location_address");
            entity.Property(e => e.LocationCity)
                .HasMaxLength(255)
                .HasColumnName("location_city");
            entity.Property(e => e.LocationDistrict)
                .HasMaxLength(255)
                .HasColumnName("location_district");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitude");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Category).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Workshop__catego__0F624AF8");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Workshop__organi__0E6E26BF");
        });

        modelBuilder.Entity<WorkshopImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Workshop__DC9AC9555C0A8683");

            entity.ToTable("WorkshopImage");

            entity.HasIndex(e => e.WorkshopId, "IX_WorkshopImage_workshop_id");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.IsPrimary)
                .HasDefaultValue(false)
                .HasColumnName("is_primary");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.WorkshopImages)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK__WorkshopI__works__10566F31");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
