using DotNetEnv;
using EXE201_Workshopista.Middlewares;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net.payOS;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
using Serilog;
using Service.Interfaces;
using Service.Interfaces.IAuth;
using Service.Interfaces.ICategory;
using Service.Interfaces.IEmailService;
using Service.Interfaces.IOTP;
using Service.Interfaces.ITicketRank;
using Service.Mapping;
using Service.Models.Firebase;
using Service.Services;
using Service.Services.Auths;
using Service.Services.Categories;
using Service.Services.Emails;
using Service.Services.Organizers;
using Service.Services.OTPs;
using Service.Services.TicketRanks;
using Service.Services.Users;
using System.Text;
using System.Text.Json.Serialization;

namespace EXE201_Workshopista
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, loggerConfig) =>
                 loggerConfig.ReadFrom.Configuration(context.Configuration));
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            Env.Load();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<FirebaseSettings>(builder.Configuration.GetSection("Firebase"));
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddSingleton<PayOS>(provider =>
            {
                string clientId = builder.Configuration["PaymentEnvironment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find PAYOS_CLIENT_ID");
                string apiKey = builder.Configuration["PaymentEnvironment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find PAYOS_API_KEY");
                string checksumKey = builder.Configuration["PaymentEnvironment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find PAYOS_CHECKSUM_KEY");
 
                return new PayOS(clientId, apiKey, checksumKey);
            });
            builder.Services.AddScoped<IAuthService, AuthService>();
            //DI
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IOTPRepository, OTPRepository>();
            builder.Services.AddScoped<IOTPService, OTPService>();
            builder.Services.AddScoped<IOrganizerService, OrganizerService>();
            builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            builder.Services.AddScoped<IWorkshopService, WorkshopService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            builder.Services.AddScoped<ITicketRankRepository, TicketRankRepository>();
            builder.Services.AddScoped<ITicketRankService, TicketRankService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<ICommissionRepository, CommissionRepository>();
            builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
            builder.Services.AddScoped<ISubscriptionTransactionRepository, SubscriptionTransactionRepository>();
            builder.Services.AddScoped<ICommissionTransactionRepository, CommissionTransactionRepository>();
            builder.Services.AddScoped<IPromotionTransactionRepository, PromotionTransactionRepository>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
            builder.Services.AddScoped<IWorkshopImageRepository, WorkshopImageRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            builder.Services.AddSwaggerGen(c =>
            {
                // Add JWT Authentication support
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddSingleton<FirebaseApp>(provider =>
            {
                var firebaseSettings = provider.GetRequiredService<IOptions<FirebaseSettings>>().Value;

                var firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                    ProjectId = firebaseSettings.ProjectId,
                    ServiceAccountId = firebaseSettings.ServiceAccountId
                });

                return firebaseApp;
            });

            builder.Services.AddDbContext<Exe201WorkshopistaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBUtilsConnectionString")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Develop",
                                  policy =>
                                  {
                                      policy
                                      .WithOrigins(
                                            "http://localhost:3000", 
                                            "http://localhost:5000",  
                                            "http://127.0.0.1:3000",  
                                            "http://127.0.0.1:5000"  
                                      )
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });

                options.AddPolicy(name: "Production",
                                  policy =>
                                  {
                                      policy
                                      .WithOrigins(
                                            "http://localhost:3000",
                                            "http://localhost:5000",
                                            "http://127.0.0.1:3000",
                                            "http://127.0.0.1:5000"
                                      )
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("Develop");
            }
            else
            {
                app.UseCors("Production");
            }

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Exe201WorkshopistaContext>();
                context.Database.Migrate();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
