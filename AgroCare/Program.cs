using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using AgroCare.Controllers;
using Models.Models;
using Services;

namespace AgroCare
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;


            #region Services
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FarmCompany")!);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AgroCareIdentity")!);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IService<Plan>, PlanService>();
            services.AddScoped<IService<Purchase>, PurchaseService>();
            services.AddScoped<IService<Order>, OrderService>();
            services.AddScoped<IService<Land>, LandService>();
            services.AddScoped<IService<Step>, StepService>();
            services.AddScoped<IService<Farmer>, FarmerService>();
            services.AddScoped<IService<Store>, StoreService>();
            services.AddScoped<IService<Engineer>, EngineerService>();
            #endregion


            var app = builder.Build();


            #region Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("store-default",
                "/store",
                new { controller = "Store", action = nameof(StoreController.ShowPurchases) });
            app.MapControllerRoute("buyer-default",
                "/buyer",
                new { controller = "Buyer", action = nameof(BuyerController.ShowOrders) });
            app.MapControllerRoute("farmer-default",
                "/farmer",
                new { controller = "Farmer", action = nameof(FarmerController.ShowPlans) });
            app.MapControllerRoute("executive-default",
                "/executive",
                new { controller = "Executive", action = nameof(ExecutiveController.ReceivedTasks) });
            app.MapControllerRoute("admin-default",
                "/admin",
                new { controller = "Admin", action = nameof(AdminController.ReceivedOrders) });
            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            #endregion


            await SeedData.Seed(app);
            app.Run();
        }
    }
}