using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using AgroCare.Controllers;
using AgroCare.Data;
using Models.Models;
using Services;
using AgroCare.Hubs;

namespace AgroCare
{
    public class Program
    {
        //cout << "farag" << endl;
        //<farmerId, (code, planId)>
        public static Dictionary<int, (string, int)> PlansCodes = new();


        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;


            #region Services
            services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddNewtonsoftJson();
            services.AddRazorPages();
            services.AddSignalR();
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
            services.AddAutoMapper(typeof(Program).Assembly);

            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<PlanService>();
            services.AddScoped<PurchaseService>();
            services.AddScoped<OrderService>();
            services.AddScoped<LandService>();
            services.AddScoped<StepService>();
            services.AddScoped<FarmerService>();
            services.AddScoped<StoreService>();
            services.AddScoped<EngineerService>();
            services.AddScoped(typeof(UserIdService<>));
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
                new { controller = "Buyer", action = nameof(BuyerController.ShowPendingOrders) });
            app.MapControllerRoute("farmer-default",
                "/farmer",
                new { controller = "Farmer", action = nameof(FarmerController.ShowPlans) });
            app.MapControllerRoute("executive-default",
                "/executive",
                new { controller = "Database", action = nameof(DatabaseController.ViewPlans) });
            app.MapControllerRoute("admin-default",
                "/admin",
                new { controller = "Database", action = nameof(DatabaseController.ViewFarmers) });
            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            app.MapHub<BuyerAdminHub>($"/{nameof(BuyerAdminHub)}");
            #endregion


            await SeedData.Seed(app);
            app.Run();
        }
    }
}