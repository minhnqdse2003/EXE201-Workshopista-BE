using System;
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
                    entity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    performed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__9E2397E0273670BF", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__D54EE9B420B124AB", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    news_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    published_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__News__4C27CCD8D61AD16C", x => x.news_id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    method_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__8A3EA9EBF19ACE4A", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    profile_image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email_verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    phone_verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    refresh_token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__B9BE370F4336E3EF", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    payment_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    payment_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__46596229D5600F2F", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Order__participa__1AD3FDA4",
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
                    organization_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    website_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    social_links = table.Column<string>(type: "text", nullable: true),
                    verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organize__06347014B8E42884", x => x.organizer_id);
                    table.ForeignKey(
                        name: "FK__Organizer__user___0D7A0286",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.id);
                    table.ForeignKey(
                        name: "FK_OTP_User",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    subscription_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    auto_renew = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__863A7EC1993FA16B", x => x.subscription_id);
                    table.ForeignKey(
                        name: "FK__Subscript__user___236943A5",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__85C600AF4A720ABE", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_Transaction_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Transacti__payme__14270015",
                        column: x => x.payment_method_id,
                        principalTable: "PaymentMethod",
                        principalColumn: "payment_method_id");
                });

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    location_city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    location_district = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    location_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: true),
                    video_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Workshop__EA6B0559D8B60A03", x => x.workshop_id);
                    table.ForeignKey(
                        name: "FK__Workshop__catego__0F624AF8",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK__Workshop__organi__0E6E26BF",
                        column: x => x.organizer_id,
                        principalTable: "Organizer",
                        principalColumn: "organizer_id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTransaction",
                columns: table => new
                {
                    subscription_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscription_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__762A0D4C91ACD385", x => x.subscription_transaction_id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_Subscription",
                        column: x => x.subscription_id,
                        principalTable: "Subscription",
                        principalColumn: "subscription_id");
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_Transaction",
                        column: x => x.transaction_id,
                        principalTable: "Transaction",
                        principalColumn: "transaction_id");
                });

            migrationBuilder.CreateTable(
                name: "Commission",
                columns: table => new
                {
                    commission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    commission_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    total_commission = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Commissi__D19D7CC90A4035D4", x => x.commission_id);
                    table.ForeignKey(
                        name: "FK__Commissio__works__2645B050",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "CommissionTransaction",
                columns: table => new
                {
                    commission_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    commission_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Commissi__32A99DDF3CA1F70E", x => x.commission_transaction_id);
                    table.ForeignKey(
                        name: "FK_CommissionTransaction_Transaction",
                        column: x => x.transaction_id,
                        principalTable: "Transaction",
                        principalColumn: "transaction_id");
                    table.ForeignKey(
                        name: "FK_CommissionTransaction_Workshop",
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
                    total_revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    average_rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true),
                    total_reviews = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventAna__D5DC3DE1912B45AA", x => x.analytics_id);
                    table.ForeignKey(
                        name: "FK__EventAnal__works__22751F6C",
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
                    promotion_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__2CB9556B36D75614", x => x.promotion_id);
                    table.ForeignKey(
                        name: "FK__Promotion__organ__245D67DE",
                        column: x => x.organizer_id,
                        principalTable: "Organizer",
                        principalColumn: "organizer_id");
                    table.ForeignKey(
                        name: "FK__Promotion__works__25518C17",
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
                    review_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__60883D90378C5259", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Review_User",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Review__workshop__208CD6FA",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "TicketRank",
                columns: table => new
                {
                    ticket_rank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    rank_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TicketRa__1B8160B1D43B79A1", x => x.ticket_rank_id);
                    table.ForeignKey(
                        name: "FK_TicketRank_Workshop",
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
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Workshop__DC9AC9555C0A8683", x => x.image_id);
                    table.ForeignKey(
                        name: "FK__WorkshopI__works__10566F31",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "PromotionTransaction",
                columns: table => new
                {
                    promotion_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    promotion_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    workshop_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__A5C4F6151910D125", x => x.promotion_transaction_id);
                    table.ForeignKey(
                        name: "FK_PromotionTransaction_Promotion",
                        column: x => x.promotion_id,
                        principalTable: "Promotion",
                        principalColumn: "promotion_id");
                    table.ForeignKey(
                        name: "FK_PromotionTransaction_Transaction",
                        column: x => x.transaction_id,
                        principalTable: "Transaction",
                        principalColumn: "transaction_id");
                    table.ForeignKey(
                        name: "FK_PromotionTransaction_Workshop",
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
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__F6FB5AE4C762AF40", x => x.order_details_id);
                    table.ForeignKey(
                        name: "FK__OrderDeta__order__1BC821DD",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__OrderDeta__works__1CBC4616",
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
                    order_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ticket_rank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currency_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    qr_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    payment_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__D596F96B7099B05A", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_Ticket_OrderDetails",
                        column: x => x.order_detail_id,
                        principalTable: "OrderDetails",
                        principalColumn: "order_details_id");
                    table.ForeignKey(
                        name: "FK_Ticket_TicketRank",
                        column: x => x.ticket_rank_id,
                        principalTable: "TicketRank",
                        principalColumn: "ticket_rank_id");
                    table.ForeignKey(
                        name: "FK__Ticket__workshop__114A936A",
                        column: x => x.workshop_id,
                        principalTable: "Workshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "category_id", "created_at", "description", "name", "slug", "status", "updated_at" },
                values: new object[,]
                {
                    { new Guid("27ae5c53-f0b7-4f71-b52a-52b78511fb01"), new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3675), "Workshops on software development, AI, cloud computing, and emerging technologies.", "Technology", "technology", "Active", new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3676) },
                    { new Guid("3ddf752c-eed5-42c8-93b9-4cd316135a19"), new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3701), "Workshops aimed at personal growth, leadership, and career development.", "Personal Development", "personal-development", "Active", new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3701) },
                    { new Guid("8f40d269-eaa5-42d7-9d15-97d9bb147a6d"), new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3695), "Creative workshops covering arts, crafts, and design.", "Arts & Crafts", "arts-and-crafts", "Active", new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3696) },
                    { new Guid("c74797ba-bb5d-46ea-9574-8b7dbe75c4f3"), new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3667), "Workshops focused on business skills, entrepreneurship, and management.", "Business", "business", "Active", new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3672) },
                    { new Guid("e1670a2c-2ea1-474a-a882-28c575df7f81"), new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3698), "Workshops focused on fitness, mental health, and overall well-being.", "Health & Wellness", "health-wellness", "Active", new DateTime(2024, 9, 30, 5, 59, 1, 369, DateTimeKind.Utc).AddTicks(3699) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "email", "email_verified", "first_name", "last_name", "password_hash", "phone_number", "phone_verified", "profile_image_url", "refresh_token", "refresh_token_expiry_time", "role", "status" },
                values: new object[,]
                {
                    { new Guid("30c18893-99f0-49b9-98fb-18a990489f87"), "admin@gmail.com", true, "Alice", "Smith", "$2a$11$ASMuiRckojlJiy4wEYzSU.dZ3mrYOGvO0QmfkD8I/H0kgSkMJQqiK", "1234567890", true, "https://i0.wp.com/fdlc.org/wp-content/uploads/2021/01/157-1578186_user-profile-default-image-png-clipart.png.jpeg?fit=880%2C769&ssl=1", null, null, "Admin", "Active" },
                    { new Guid("382359c6-916d-4548-8583-1a0807f57b7e"), "org@gmail.com", true, "Bob", "Johnson", "$2a$11$hClCjcawkPoEaPyI8VO3RuH592yCe53DtEvPe/aRPk6bKKROCGREO", "9876543210", true, "https://i0.wp.com/fdlc.org/wp-content/uploads/2021/01/157-1578186_user-profile-default-image-png-clipart.png.jpeg?fit=880%2C769&ssl=1", null, null, "Organizer", "Active" },
                    { new Guid("70cfded3-8d98-4a54-ac62-ff6478450c3b"), "charlie@example.com", true, "Charlie", "Brown", "$2a$11$BopGOIlJDnHg8v3Fv6ROxOEEDsYiEwSW683DSMfy6CST1TQS8JvvK", "5551234567", true, "https://i0.wp.com/fdlc.org/wp-content/uploads/2021/01/157-1578186_user-profile-default-image-png-clipart.png.jpeg?fit=880%2C769&ssl=1", null, null, "Organizer", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Category__32DD1E4C475F73D5",
                table: "Category",
                column: "slug",
                unique: true,
                filter: "([slug] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__72E12F1B8F3F9A5C",
                table: "Category",
                column: "name",
                unique: true,
                filter: "([name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Commission_workshop_id",
                table: "Commission",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransaction_transaction_id",
                table: "CommissionTransaction",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransaction_workshop_id",
                table: "CommissionTransaction",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventAnalytics_workshop_id",
                table: "EventAnalytics",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "UQ__News__32DD1E4CEEFB436D",
                table: "News",
                column: "slug",
                unique: true,
                filter: "([slug] IS NOT NULL)");

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
                name: "IX_OTP_CreatedBy",
                table: "OTP",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_organizer_id",
                table: "Promotion",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_workshop_id",
                table: "Promotion",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionTransaction_promotion_id",
                table: "PromotionTransaction",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionTransaction_transaction_id",
                table: "PromotionTransaction",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionTransaction_workshop_id",
                table: "PromotionTransaction",
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
                name: "IX_SubscriptionTransaction_subscription_id",
                table: "SubscriptionTransaction",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransaction_transaction_id",
                table: "SubscriptionTransaction",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_order_detail_id",
                table: "Ticket",
                column: "order_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ticket_rank_id",
                table: "Ticket",
                column: "ticket_rank_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_workshop_id",
                table: "Ticket",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRank_workshop_id",
                table: "TicketRank",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_payment_method_id",
                table: "Transaction",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_user_id",
                table: "Transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616457DE1028",
                table: "User",
                column: "email",
                unique: true,
                filter: "([email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_category_id",
                table: "Workshop",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_organizer_id",
                table: "Workshop",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Workshop__32DD1E4CC1ABD095",
                table: "Workshop",
                column: "slug",
                unique: true,
                filter: "([slug] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopImage_workshop_id",
                table: "WorkshopImage",
                column: "workshop_id");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__ticke__1DB06A4F",
                table: "OrderDetails",
                column: "ticket_id",
                principalTable: "Ticket",
                principalColumn: "ticket_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__works__1CBC4616",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK__Ticket__workshop__114A936A",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRank_Workshop",
                table: "TicketRank");

            migrationBuilder.DropForeignKey(
                name: "FK__Order__participa__1AD3FDA4",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__order__1BC821DD",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__ticke__1DB06A4F",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Commission");

            migrationBuilder.DropTable(
                name: "CommissionTransaction");

            migrationBuilder.DropTable(
                name: "EventAnalytics");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "PromotionTransaction");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SubscriptionTransaction");

            migrationBuilder.DropTable(
                name: "WorkshopImage");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "TicketRank");
        }
    }
}
