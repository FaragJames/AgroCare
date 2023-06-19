﻿using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroCare
{
    public class SeedData
    {
        static readonly string[] names = { "farmer", "buyer", "center", "admin", "executive" };
        static readonly string password = "Secert123$";



        public static async Task Seed(IApplicationBuilder app)
        {
            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var appIdentity = services.GetService<AppIdentityDbContext>();
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            var userManager = services.GetService<UserManager<IdentityUser>>();

            if(appIdentity != null)
            {
                if((await appIdentity.Database.GetPendingMigrationsAsync()).Any())
                    await appIdentity.Database.MigrateAsync();

                if (!appIdentity.Roles.Any() && roleManager != null)
                {
                    await roleManager.CreateAsync(new("Buyer"));
                    await roleManager.CreateAsync(new("Admin"));
                    await roleManager.CreateAsync(new("Executive"));
                    await roleManager.CreateAsync(new("Farmer"));
                    await roleManager.CreateAsync(new("Center"));
                }

                if (userManager != null && !appIdentity.Users.Any())
                {
                    IdentityUser user;
                    foreach (var name in names)
                    {
                        user = new()
                        {
                            Email = $"{name}1@gmail.com",
                            UserName = $"{name}1"
                        };
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, $"{char.ToUpper(name[0]) + name[1..]}");
                    }
                }
            }
        }
    }
}
