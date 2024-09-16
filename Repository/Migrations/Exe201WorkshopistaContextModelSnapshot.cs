﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Models;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(Exe201WorkshopistaContext))]
    partial class Exe201WorkshopistaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repository.Models.AuditLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("log_id");

                    b.Property<string>("Action")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("action");

                    b.Property<string>("Entity")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("entity");

                    b.Property<Guid?>("EntityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("entity_id");

                    b.Property<Guid?>("PerformedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("performed_by");

                    b.Property<DateTime?>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("LogId")
                        .HasName("PK__AuditLog__9E2397E01111547E");

                    b.ToTable("AuditLog", (string)null);
                });

            modelBuilder.Entity("Repository.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("slug");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("CategoryId")
                        .HasName("PK__Category__D54EE9B4DDFAF6E2");

                    b.HasIndex(new[] { "Slug" }, "UQ__Category__32DD1E4C135F590A")
                        .IsUnique()
                        .HasFilter("[slug] IS NOT NULL");

                    b.HasIndex(new[] { "Name" }, "UQ__Category__72E12F1B3A211C2C")
                        .IsUnique()
                        .HasFilter("[name] IS NOT NULL");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Repository.Models.EventAnalytic", b =>
                {
                    b.Property<Guid>("AnalyticsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("analytics_id");

                    b.Property<decimal?>("AverageRating")
                        .HasColumnType("decimal(2, 1)")
                        .HasColumnName("average_rating");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal?>("TotalRevenue")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total_revenue");

                    b.Property<int?>("TotalReviews")
                        .HasColumnType("int")
                        .HasColumnName("total_reviews");

                    b.Property<int?>("TotalTicketsSold")
                        .HasColumnType("int")
                        .HasColumnName("total_tickets_sold");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("WorkshopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("workshop_id");

                    b.HasKey("AnalyticsId")
                        .HasName("PK__EventAna__D5DC3DE1FAF3AB89");

                    b.HasIndex("WorkshopId");

                    b.ToTable("EventAnalytics");
                });

            modelBuilder.Entity("Repository.Models.News", b =>
                {
                    b.Property<Guid>("NewsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("news_id");

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("image_url");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("published_at");

                    b.Property<string>("Slug")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("slug");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("NewsId")
                        .HasName("PK__News__4C27CCD853583D15");

                    b.HasIndex(new[] { "Slug" }, "UQ__News__32DD1E4C151519C4")
                        .IsUnique()
                        .HasFilter("[slug] IS NOT NULL");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("participant_id");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("payment_status");

                    b.Property<DateTime?>("PaymentTime")
                        .HasColumnType("datetime")
                        .HasColumnName("payment_time");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total_amount");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("OrderId")
                        .HasName("PK__Order__46596229690060C8");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Repository.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_details_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("price");

                    b.Property<int?>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("quantity");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ticket_id");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total_price");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("WorkshopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("workshop_id");

                    b.HasKey("OrderDetailsId")
                        .HasName("PK__OrderDet__F6FB5AE40F937559");

                    b.HasIndex("OrderId");

                    b.HasIndex("TicketId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Repository.Models.Organizer", b =>
                {
                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("organizer_id");

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contact_email");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("contact_phone");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("OrganizationName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("organization_name");

                    b.Property<string>("SocialLinks")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("social_links");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<bool?>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("verified");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("website_url");

                    b.HasKey("OrganizerId")
                        .HasName("PK__Organize__06347014926AE89D");

                    b.HasIndex("UserId");

                    b.ToTable("Organizer", (string)null);
                });

            modelBuilder.Entity("Repository.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payment_method_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("MethodName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("method_name");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("PaymentMethodId")
                        .HasName("PK__PaymentM__8A3EA9EBDDCC480A");

                    b.ToTable("PaymentMethod", (string)null);
                });

            modelBuilder.Entity("Repository.Models.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("review_id");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("participant_id");

                    b.Property<short?>("Rating")
                        .HasColumnType("smallint")
                        .HasColumnName("rating");

                    b.Property<string>("ReviewStatus")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("review_status");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("WorkshopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("workshop_id");

                    b.HasKey("ReviewId")
                        .HasName("PK__Review__60883D90BBCF3137");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("Repository.Models.Ticket", b =>
                {
                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ticket_id");

                    b.Property<DateTime?>("BookingTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("booking_time")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("participant_id");

                    b.Property<DateTime?>("PaymentTime")
                        .HasColumnType("datetime")
                        .HasColumnName("payment_time");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("price");

                    b.Property<string>("QrCode")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("qr_code");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<Guid?>("WorkshopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("workshop_id");

                    b.HasKey("TicketId")
                        .HasName("PK__Ticket__D596F96BAA96C801");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("Repository.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("transaction_id");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("participant_id");

                    b.Property<Guid?>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payment_method_id");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ticket_id");

                    b.Property<string>("TransactionReference")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("transaction_reference");

                    b.Property<string>("TransactionStatus")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("transaction_status");

                    b.Property<DateTime?>("TransactionTime")
                        .HasColumnType("datetime")
                        .HasColumnName("transaction_time");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("TransactionId")
                        .HasName("PK__Transact__85C600AFBDF9DFBF");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("TicketId");

                    b.HasIndex(new[] { "TransactionReference" }, "UQ__Transact__F0DAF2E855072DED")
                        .IsUnique()
                        .HasFilter("[transaction_reference] IS NOT NULL");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("Repository.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<bool?>("EmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("email_verified");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone_number");

                    b.Property<bool?>("PhoneVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("phone_verified");

                    b.Property<string>("ProfileImageUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("profile_image_url");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("UserId")
                        .HasName("PK__User__B9BE370FB245A9CC");

                    b.HasIndex(new[] { "Email" }, "UQ__User__AB6E61647CFB7D18")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Repository.Models.Workshop", b =>
                {
                    b.Property<Guid>("WorkshopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("workshop_id");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int")
                        .HasColumnName("capacity");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime")
                        .HasColumnName("end_time");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("image_url");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(10, 8)")
                        .HasColumnName("latitude");

                    b.Property<string>("LocationAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("location_address");

                    b.Property<string>("LocationCity")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("location_city");

                    b.Property<string>("LocationDistrict")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("location_district");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(11, 8)")
                        .HasColumnName("longitude");

                    b.Property<Guid?>("OrganizerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("organizer_id");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("price");

                    b.Property<string>("Slug")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("slug");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime")
                        .HasColumnName("start_time");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("video_url");

                    b.HasKey("WorkshopId")
                        .HasName("PK__Workshop__EA6B05592F621DA2");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrganizerId");

                    b.HasIndex(new[] { "Slug" }, "UQ__Workshop__32DD1E4C022E4DB2")
                        .IsUnique()
                        .HasFilter("[slug] IS NOT NULL");

                    b.ToTable("Workshop", (string)null);
                });

            modelBuilder.Entity("Repository.Models.EventAnalytic", b =>
                {
                    b.HasOne("Repository.Models.Workshop", "Workshop")
                        .WithMany("EventAnalytics")
                        .HasForeignKey("WorkshopId")
                        .HasConstraintName("FK__EventAnal__works__7C4F7684");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.HasOne("Repository.Models.User", "Participant")
                        .WithMany("Orders")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK__Order__participa__787EE5A0");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("Repository.Models.OrderDetail", b =>
                {
                    b.HasOne("Repository.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__order__797309D9");

                    b.HasOne("Repository.Models.Ticket", "Ticket")
                        .WithMany("OrderDetails")
                        .HasForeignKey("TicketId")
                        .HasConstraintName("FK__OrderDeta__ticke__7B5B524B");

                    b.HasOne("Repository.Models.Workshop", "Workshop")
                        .WithMany("OrderDetails")
                        .HasForeignKey("WorkshopId")
                        .HasConstraintName("FK__OrderDeta__works__7A672E12");

                    b.Navigation("Order");

                    b.Navigation("Ticket");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Repository.Models.Organizer", b =>
                {
                    b.HasOne("Repository.Models.User", "User")
                        .WithMany("Organizers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Organizer__user___07C12930");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.Models.Review", b =>
                {
                    b.HasOne("Repository.Models.User", "Participant")
                        .WithMany("Reviews")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK__Review__particip__778AC167");

                    b.HasOne("Repository.Models.Workshop", "Workshop")
                        .WithMany("Reviews")
                        .HasForeignKey("WorkshopId")
                        .HasConstraintName("FK__Review__workshop__76969D2E");

                    b.Navigation("Participant");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Repository.Models.Ticket", b =>
                {
                    b.HasOne("Repository.Models.User", "Participant")
                        .WithMany("Tickets")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK__Ticket__particip__72C60C4A");

                    b.HasOne("Repository.Models.Workshop", "Workshop")
                        .WithMany("Tickets")
                        .HasForeignKey("WorkshopId")
                        .HasConstraintName("FK__Ticket__workshop__71D1E811");

                    b.Navigation("Participant");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Repository.Models.Transaction", b =>
                {
                    b.HasOne("Repository.Models.User", "Participant")
                        .WithMany("Transactions")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK__Transacti__parti__74AE54BC");

                    b.HasOne("Repository.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentMethodId")
                        .HasConstraintName("FK__Transacti__payme__75A278F5");

                    b.HasOne("Repository.Models.Ticket", "Ticket")
                        .WithMany("Transactions")
                        .HasForeignKey("TicketId")
                        .HasConstraintName("FK__Transacti__ticke__73BA3083");

                    b.Navigation("Participant");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Repository.Models.Workshop", b =>
                {
                    b.HasOne("Repository.Models.Category", "Category")
                        .WithMany("Workshops")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Workshop__catego__70DDC3D8");

                    b.HasOne("Repository.Models.Organizer", "Organizer")
                        .WithMany("Workshops")
                        .HasForeignKey("OrganizerId")
                        .HasConstraintName("FK__Workshop__organi__6FE99F9F");

                    b.Navigation("Category");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Repository.Models.Category", b =>
                {
                    b.Navigation("Workshops");
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Repository.Models.Organizer", b =>
                {
                    b.Navigation("Workshops");
                });

            modelBuilder.Entity("Repository.Models.PaymentMethod", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Repository.Models.Ticket", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Repository.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Organizers");

                    b.Navigation("Reviews");

                    b.Navigation("Tickets");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Repository.Models.Workshop", b =>
                {
                    b.Navigation("EventAnalytics");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
