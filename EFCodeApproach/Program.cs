using EFCodeApproach.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCodeApproach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // We are iniating the builder services and Configuration services for our project  along with Sql Server Database Connection
            var provider = builder.Services.BuildServiceProvider();
            var config=provider.GetService<IConfiguration>();

            // Here we are using our Context Class through whch we are making DB Connection 
            builder.Services.AddDbContext<StudentDbContext>(item=>item.UseSqlServer(config.GetConnectionString("db")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}