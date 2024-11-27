
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MNF_PORTAL_Core;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Infrastructure;
using MNF_PORTAL_Infrastructure.Data;
using MNF_PORTAL_Infrastructure.Implementation_Repos;
using MNF_PORTAL_Infrastructure.Repositories;
using MNF_PORTAL_Service.Interfaces;
using MNF_PORTAL_Service.Services;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace MNF_PORTAL_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            options.SuppressModelStateInvalidFilter = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            #region Swagger Setting
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User and Role Management API",
                    Description = " Portal Project "
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
         new OpenApiSecurityScheme
         {
         Reference = new OpenApiReference
         {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
         }
         },
         new string[] {}
         }
         });
            });
            #endregion
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddTransient<IUserRepository,UserRepository>();
            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddScoped<IdentitySeeder>();


            /*====================================== Jwt Settings ======================================*/

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

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
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

                    });
            });




            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
                await seeder.SeedAsync();
            }

            app.Run();
        }
    }
}
