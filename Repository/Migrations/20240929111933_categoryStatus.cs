using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class categoryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Workshop__32DD1E4CC1ABD095",
                table: "Workshop");

            migrationBuilder.DropIndex(
                name: "UQ__User__AB6E616457DE1028",
                table: "User");

            migrationBuilder.DropIndex(
                name: "UQ__News__32DD1E4CEEFB436D",
                table: "News");

            migrationBuilder.DropIndex(
                name: "UQ__Category__32DD1E4C475F73D5",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "UQ__Category__72E12F1B8F3F9A5C",
                table: "Category");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: new Guid("2cb3ae50-9347-48cb-9e4a-3b68311e6ba6"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: new Guid("71d81979-db02-40da-ac10-bd9e83088e9a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: new Guid("83a28e59-0311-453b-99b0-abaad0813652"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: new Guid("8796fcb7-455d-49f4-967b-19608547d4f9"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: new Guid("bcac2bd7-16f0-4b59-9ad8-15bdabb90859"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "user_id",
                keyValue: new Guid("10477a82-d4e0-4558-ae41-4308213adc54"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "user_id",
                keyValue: new Guid("bfc7ddac-eceb-455f-9f10-a306022112bd"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "user_id",
                keyValue: new Guid("e335a7f0-0c6a-4256-a885-2d54a11e8c4e"));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Category",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "UQ__Workshop__32DD1E4CC1ABD095",
                table: "Workshop",
                column: "slug",
                unique: true,
                filter: "([slug] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616457DE1028",
                table: "User",
                column: "email",
                unique: true,
                filter: "([email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__News__32DD1E4CEEFB436D",
                table: "News",
                column: "slug",
                unique: true,
                filter: "([slug] IS NOT NULL)");

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
                name: "IX_OTP_CreatedBy",
                table: "OTP",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropIndex(
                name: "UQ__Workshop__32DD1E4CC1ABD095",
                table: "Workshop");

            migrationBuilder.DropIndex(
                name: "UQ__User__AB6E616457DE1028",
                table: "User");

            migrationBuilder.DropIndex(
                name: "UQ__News__32DD1E4CEEFB436D",
                table: "News");

            migrationBuilder.DropIndex(
                name: "UQ__Category__32DD1E4C475F73D5",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "UQ__Category__72E12F1B8F3F9A5C",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Category");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "category_id", "created_at", "description", "name", "slug", "updated_at" },
                values: new object[,]
                {
                    { new Guid("2cb3ae50-9347-48cb-9e4a-3b68311e6ba6"), new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6958), "Workshops on software development, AI, cloud computing, and emerging technologies.", "Technology", "technology", new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6959) },
                    { new Guid("71d81979-db02-40da-ac10-bd9e83088e9a"), new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6966), "Workshops focused on fitness, mental health, and overall well-being.", "Health & Wellness", "health-wellness", new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6966) },
                    { new Guid("83a28e59-0311-453b-99b0-abaad0813652"), new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6968), "Workshops aimed at personal growth, leadership, and career development.", "Personal Development", "personal-development", new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6968) },
                    { new Guid("8796fcb7-455d-49f4-967b-19608547d4f9"), new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6946), "Workshops focused on business skills, entrepreneurship, and management.", "Business", "business", new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6953) },
                    { new Guid("bcac2bd7-16f0-4b59-9ad8-15bdabb90859"), new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6962), "Creative workshops covering arts, crafts, and design.", "Arts & Crafts", "arts-and-crafts", new DateTime(2024, 9, 26, 9, 1, 41, 34, DateTimeKind.Utc).AddTicks(6964) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "email", "email_verified", "first_name", "last_name", "password_hash", "phone_number", "phone_verified", "profile_image_url", "refresh_token", "refresh_token_expiry_time", "role", "status" },
                values: new object[,]
                {
                    { new Guid("10477a82-d4e0-4558-ae41-4308213adc54"), "org@gmail.com", true, "Bob", "Johnson", "$2a$11$w9o0TwVi8jmx7e8UAWStg.anwRYY5Ci5I4ATrgFiiiYOBa/PJwZWe", "9876543210", true, "https://example.com/profile_image_2.jpg", null, null, "Organizer", null },
                    { new Guid("bfc7ddac-eceb-455f-9f10-a306022112bd"), "admin@gmail.com", true, "Alice", "Smith", "$2a$11$8l8fT6roQZiAPL9JtmdlwOsb/gj7mPyfxZ6Cz7Z8xypr.JmFj.mU2", "1234567890", true, "https://example.com/profile_image_1.jpg", null, null, "Admin", null },
                    { new Guid("e335a7f0-0c6a-4256-a885-2d54a11e8c4e"), "charlie@example.com", true, "Charlie", "Brown", "$2a$11$deYf4feSzglUn7mARRLWnehQYj3J14iCXYQt5DKIMk/jBqgx6g5/q", "5551234567", true, "https://example.com/profile_image_3.jpg", null, null, "Organizer", null }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Workshop__32DD1E4CC1ABD095",
                table: "Workshop",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616457DE1028",
                table: "User",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__News__32DD1E4CEEFB436D",
                table: "News",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__32DD1E4C475F73D5",
                table: "Category",
                column: "slug",
                unique: true,
                filter: "[slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__72E12F1B8F3F9A5C",
                table: "Category",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");
        }
    }
}
