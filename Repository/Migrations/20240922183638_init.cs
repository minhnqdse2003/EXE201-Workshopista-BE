﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    log_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    performed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__9E2397E0775D0C7B", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__D54EE9B4999D4519", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    news_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    published_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__News__4C27CCD8F7E3AF43", x => x.news_id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    method_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__8A3EA9EBA7FDCED2", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    profile_image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email_verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    phone_verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__B9BE370F02E16F77", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    payment_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    payment_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__465962298BD6BFF4", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    organizer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    organization_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    contact_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    contact_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    website_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    social_links = table.Column<string>(type: "text", nullable: true),
                    verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organize__063470141F69041A", x => x.organizer_id);
                    table.ForeignKey(
                        name: "FK_Organizer_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    subscription_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tier = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    auto_renew = table.Column<bool>(type: "bit", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__863A7EC1425E97CF", x => x.subscription_id);
                    table.ForeignKey(
                        name: "FK_Subscription_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    location_city = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    location_district = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    location_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: true),
                    video_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Workshop__EA6B0559AAAD1465", x => x.workshop_id);
                    table.ForeignKey(
                        name: "FK_Workshop_Category",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK_Workshop_Organizer",
                        column: x => x.organizer_id,
                        principalTable: "Organizer",
                        principalColumn: "organizer_id");
                });

            migrationBuilder.CreateTable(
                name: "Commission",
                columns: table => new
                {
                    commission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    commission_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    total_commission = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Commissi__D19D7CC994FB9C82", x => x.commission_id);
                    table.ForeignKey(
                        name: "FK_Commission_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "EventAnalytics",
                columns: table => new
                {
                    analytics_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    total_tickets_sold = table.Column<int>(type: "int", nullable: true),
                    total_revenue = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    average_rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true),
                    total_reviews = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventAna__D5DC3DE1137F8C0D", x => x.analytics_id);
                    table.ForeignKey(
                        name: "FK_EventAnalytics_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    promotion_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    promotion_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__2CB9556B2C8CD7C8", x => x.promotion_id);
                    table.ForeignKey(
                        name: "FK_Promotion_Organizer",
                        column: x => x.organizer_id,
                        principalTable: "Organizer",
                        principalColumn: "organizer_id");
                    table.ForeignKey(
                        name: "FK_Promotion_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    rating = table.Column<short>(type: "smallint", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    review_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__60883D90FB2B51AA", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Review_Participant",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Review_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    qr_code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    payment_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    booking_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__D596F96BCA20FFB1", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_Ticket_Participant",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Ticket_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "WorkshopImage",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Workshop__DC9AC95561A32F46", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_WorkshopImage_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    order_details_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__F6FB5AE44C7C30C5", x => x.order_details_id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Ticket",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "ticket_id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Workshop",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    transaction_reference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    transaction_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__85C600AFBC29D7EE", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_Transaction_Participant",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Transaction_PaymentMethod",
                        column: x => x.payment_method_id,
                        principalTable: "PaymentMethod",
                        principalColumn: "payment_method_id");
                    table.ForeignKey(
                        name: "FK_Transaction_Ticket",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "ticket_id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "email", "email_verified", "first_name", "last_name", "password_hash", "phone_number", "phone_verified", "profile_image_url", "refresh_token", "refresh_token_expiry_time", "role" },
                values: new object[,]
                {
                    { new Guid("4152f4cc-dba0-466b-84c4-30cc256f1c4f"), "charlie@example.com", true, "Charlie", "Brown", "$2a$11$v9fBhxAbETNORWTAijyh6.b8d/7DWnhVGzLfpWHKcjZ/x7ehy2YTm", "5551234567", true, "https://example.com/profile_image_3.jpg", null, null, "Organizer" },
                    { new Guid("45fe7490-9058-41ec-99c5-e3c806d8763c"), "org@gmail.com", true, "Bob", "Johnson", "$2a$11$nRoy13hETCslCWMu/JJtWOXRyeXLU18ItxcQbDYuVAmmsIzczWTly", "9876543210", true, "https://example.com/profile_image_2.jpg", null, null, "Organizer" },
                    { new Guid("96173225-e65f-493a-a69a-eb3b688a581b"), "admin@gmail.com", true, "Alice", "Smith", "$2a$11$FR2Ki3YCqkFCsdULGZZPiOfOWzqWBy56BMuPz/MjqeyMOxDQ3xk8W", "1234567890", true, "https://example.com/profile_image_1.jpg", null, null, "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Category__32DD1E4CA9B28410",
                table: "Category",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__72E12F1BF3C908B9",
                table: "Category",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Commission_workshop_id",
                table: "Commission",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventAnalytics_workshop_id",
                table: "EventAnalytics",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "UQ__News__32DD1E4C5A8B7C4F",
                table: "News",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_participant_id",
                table: "Order",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_order_id",
                table: "OrderDetails",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ticket_id",
                table: "OrderDetails",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_workshop_id",
                table: "OrderDetails",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Organizer_user_id",
                table: "Organizer",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_organizer_id",
                table: "Promotion",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_workshop_id",
                table: "Promotion",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_participant_id",
                table: "Review",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_workshop_id",
                table: "Review",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_user_id",
                table: "Subscription",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_participant_id",
                table: "Ticket",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_workshop_id",
                table: "Ticket",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_participant_id",
                table: "Transaction",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_payment_method_id",
                table: "Transaction",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ticket_id",
                table: "Transaction",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Transact__F0DAF2E82F8DC13B",
                table: "Transaction",
                column: "transaction_reference",
                unique: true,
                filter: "[transaction_reference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164168C33A0",
                table: "User",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_category_id",
                table: "Workshop",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_organizer_id",
                table: "Workshop",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Workshop__32DD1E4C1ED9DC10",
                table: "Workshop",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopImage_workshop_id",
                table: "WorkshopImage",
                column: "workshop_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Commission");

            migrationBuilder.DropTable(
                name: "EventAnalytics");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "WorkshopImage");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}