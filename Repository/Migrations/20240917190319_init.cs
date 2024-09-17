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
                    entity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    performed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__9E2397E01111547E", x => x.log_id);
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
                    table.PrimaryKey("PK__Category__D54EE9B4DDFAF6E2", x => x.category_id);
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
                    table.PrimaryKey("PK__News__4C27CCD853583D15", x => x.news_id);
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
                    table.PrimaryKey("PK__PaymentM__8A3EA9EBDDCC480A", x => x.payment_method_id);
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
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__B9BE370FB245A9CC", x => x.user_id);
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
                    table.PrimaryKey("PK__Order__46596229690060C8", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Order__participa__787EE5A0",
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
                    social_links = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organize__06347014926AE89D", x => x.organizer_id);
                    table.ForeignKey(
                        name: "FK__Organizer__user___07C12930",
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
                    image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    video_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Workshop__EA6B05592F621DA2", x => x.workshop_id);
                    table.ForeignKey(
                        name: "FK__Workshop__catego__70DDC3D8",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK__Workshop__organi__6FE99F9F",
                        column: x => x.organizer_id,
                        principalTable: "Organizer",
                        principalColumn: "organizer_id");
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
                    table.PrimaryKey("PK__EventAna__D5DC3DE1FAF3AB89", x => x.analytics_id);
                    table.ForeignKey(
                        name: "FK__EventAnal__works__7C4F7684",
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
                    table.PrimaryKey("PK__Review__60883D90BBCF3137", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Review__particip__778AC167",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Review__workshop__76969D2E",
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
                    table.PrimaryKey("PK__Ticket__D596F96BAA96C801", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK__Ticket__particip__72C60C4A",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Ticket__workshop__71D1E811",
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
                    table.PrimaryKey("PK__OrderDet__F6FB5AE40F937559", x => x.order_details_id);
                    table.ForeignKey(
                        name: "FK__OrderDeta__order__797309D9",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__OrderDeta__ticke__7B5B524B",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "ticket_id");
                    table.ForeignKey(
                        name: "FK__OrderDeta__works__7A672E12",
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
                    table.PrimaryKey("PK__Transact__85C600AFBDF9DFBF", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK__Transacti__parti__74AE54BC",
                        column: x => x.participant_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Transacti__payme__75A278F5",
                        column: x => x.payment_method_id,
                        principalTable: "PaymentMethod",
                        principalColumn: "payment_method_id");
                    table.ForeignKey(
                        name: "FK__Transacti__ticke__73BA3083",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "ticket_id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "email", "email_verified", "first_name", "last_name", "password_hash", "phone_number", "phone_verified", "profile_image_url", "RefreshToken", "RefreshTokenExpiryTime", "role" },
                values: new object[,]
                {
                    { new Guid("88d9b72c-5387-4d0e-9051-9469e8c5f7b6"), "admin@gmail.com", true, "Alice", "Smith", "$2a$11$p8.zexmNkOK0t6Gw7Ocog.pDBfrxNngyyL6nx80i0WG7DSoCpjhbm", "1234567890", true, "https://example.com/profile_image_1.jpg", null, null, "admin" },
                    { new Guid("9ad88b67-870d-4a64-a508-25cc917fc0aa"), "charlie@example.com", true, "Charlie", "Brown", "$2a$11$TmY5zpQNlwtRJ5fCzIxeZ.egap4mCu8rM8ohJKumg6LFSjRCPpmTS", "5551234567", true, "https://example.com/profile_image_3.jpg", null, null, "Organizer" },
                    { new Guid("d572044d-25e0-4b06-9471-6f5f5b1789fa"), "org@gmail.com", true, "Bob", "Johnson", "$2a$11$s8JGyt8Uz/qvB71dZ/9NcO6OnsNb7Vawd3kCtjqsy4D/oZsSmNzeO", "9876543210", true, "https://example.com/profile_image_2.jpg", null, null, "Organizer" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Category__32DD1E4C135F590A",
                table: "Category",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__72E12F1B3A211C2C",
                table: "Category",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventAnalytics_workshop_id",
                table: "EventAnalytics",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "UQ__News__32DD1E4C151519C4",
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
                name: "IX_Review_participant_id",
                table: "Review",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_workshop_id",
                table: "Review",
                column: "workshop_id");

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
                name: "UQ__Transact__F0DAF2E855072DED",
                table: "Transaction",
                column: "transaction_reference",
                unique: true,
                filter: "[transaction_reference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E61647CFB7D18",
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
                name: "UQ__Workshop__32DD1E4C022E4DB2",
                table: "Workshop",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "EventAnalytics");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Transaction");

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
