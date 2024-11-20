
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace MNF_PORTAL_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /*=================== Mahmoud =====================*/
            




















            
            ////////////////////////////////////////
            /*===================== kareem ==========================*/


            //////////////////////////////////////////////////////

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
