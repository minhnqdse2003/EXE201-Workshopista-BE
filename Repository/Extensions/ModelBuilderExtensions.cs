using Microsoft.EntityFrameworkCore;
using Repository.Consts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456789"),
                    PhoneNumber = "1234567890",
                    Role = RoleConst.Admin,
                    ProfileImageUrl = "https://example.com/profile_image_1.jpg",
                    EmailVerified = true,
                    PhoneVerified = true
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "org@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456789"),
                    PhoneNumber = "9876543210",
                    Role = RoleConst.Organizer,
                    ProfileImageUrl = "https://example.com/profile_image_2.jpg",
                    EmailVerified = true,
                    PhoneVerified = true
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Charlie",
                    LastName = "Brown",
                    Email = "charlie@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456789"),
                    PhoneNumber = "5551234567",
                    Role = RoleConst.Organizer,
                    ProfileImageUrl = "https://example.com/profile_image_3.jpg",
                    EmailVerified = true,
                    PhoneVerified = true
                }
            );

            modelBuilder.Entity<Category>().HasData(
                   new Category
                   {
                       CategoryId = Guid.NewGuid(),
                       Name = "Business",
                       Slug = "business",
                       Description = "Workshops focused on business skills, entrepreneurship, and management.",
                       CreatedAt = DateTime.UtcNow,
                       UpdatedAt = DateTime.UtcNow
                   },
            new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Technology",
                Slug = "technology",
                Description = "Workshops on software development, AI, cloud computing, and emerging technologies.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Arts & Crafts",
                Slug = "arts-and-crafts",
                Description = "Creative workshops covering arts, crafts, and design.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Health & Wellness",
                Slug = "health-wellness",
                Description = "Workshops focused on fitness, mental health, and overall well-being.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Personal Development",
                Slug = "personal-development",
                Description = "Workshops aimed at personal growth, leadership, and career development.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
                );
        }
    }

}
