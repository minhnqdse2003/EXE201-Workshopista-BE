
using EXE201_Workshopista.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
using Serilog;
using Service.Interfaces;
using Service.Mapping;
using Service.Services;
using System.Text;

namespace EXE201_Workshopista
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, loggerConfig) =>
                 loggerConfig.ReadFrom.Configuration(context.Configuration));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(UserProfile));

            builder.Services.AddDbContext<Exe201WorkshopistaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBUtilsConnectionString")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Exe201WorkshopistaContext>();
                context.Database.Migrate();

                if (context.Users != null && !context.Users.Any())
                {
                    context.Users.AddRange(
                    // Sample User Account 1
                    new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = "Alice",
                        LastName = "Smith",
                        Email = "alice@example.com",
                        PasswordHash = "hashed_password_1",
                        PhoneNumber = "1234567890",
                        Role = "user",
                        ProfileImageUrl = "https://example.com/profile_image_1.jpg",
                        EmailVerified = true,
                        PhoneVerified = true
                    }

                    // Sample User Account 2
                    , new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = "Bob",
                        LastName = "Johnson",
                        Email = "bob@example.com",
                        PasswordHash = "hashed_password_2",
                        PhoneNumber = "9876543210",
                        Role = "admin",
                        ProfileImageUrl = "https://example.com/profile_image_2.jpg",
                        EmailVerified = true,
                        PhoneVerified = true
                    }
                    , new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = "Charlie",
                        LastName = "Brown",
                        Email = "charlie@example.com",
                        PasswordHash = "hashed_password_3",
                        PhoneNumber = "5551234567",
                        Role = "user",
                        ProfileImageUrl = "https://example.com/profile_image_3.jpg",
                        EmailVerified = true,
                        PhoneVerified = true
                    });
                    context.SaveChanges();
                }
            }

            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
