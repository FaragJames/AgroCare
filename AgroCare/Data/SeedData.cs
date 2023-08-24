using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Newtonsoft.Json;

namespace AgroCare.Data
{
    public class SeedData
    {
        static readonly string[] roles = { "Farmer", "Buyer", "Store", "Admin", "Executive" };
        static readonly string password = "Secert123$";



        public static async Task Seed(IApplicationBuilder app)
        {
            #region Services
            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var appIdentity = services.GetService<AppIdentityDbContext>();
            var context = services.GetService<AppDbContext>();
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            var userManager = services.GetService<UserManager<IdentityUser>>();
            #endregion


            if (appIdentity != null)
            {
                await appIdentity.Database.MigrateAsync();

                if (!appIdentity.Roles.Any() && roleManager != null)
                {
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(new(role));
                    }
                }

                if (userManager != null && !appIdentity.Users.Any())
                {
                    IdentityUser user;
                    foreach (var name in roles)
                    {
                        user = new()
                        {
                            Email = $"{name.ToLower()}_{name}@gmail.com",
                            UserName = $"{name.ToLower()}_{name}"
                        };
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, name);
                    }
                    for (int i = 1; i < 3; i++)
                    {
                        user = new()
                        {
                            Email = $"store{i}_Store@gmail.com",
                            UserName = $"store{i}_Store"
                        };
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, "Store");
                    }
                }
            }

            //string json, path = "Data/JsonDummyData/";
            if (context != null && !await context.Farmers.AnyAsync())
            {
                await context.Database.MigrateAsync();

                Models.Models.Action[] actions = new Models.Models.Action[]
                {
                    new() { Name = "Plowing" },
                    new() { Name = "Leveling" },
                    new() { Name = "Soil Testing" },
                    new() { Name = "Fertilizing" },
                    new() { Name = "Seeding" },
                    new() { Name = "Harvesting" }
                };
                AgriculturalItem[] agriculturalItems = new AgriculturalItem[]
                {
                    new() { Name = "Hoe" },
                    new() { Name = "Rake" },
                    new() { Name = "Fungicides" },
                    new() { Name = "Herbicides" },
                    new() { Name = "Harvester" },
                    new() { Name = "Tractor" },
                    new() { Name = "Ammonia" },
                    new() { Name = "Superphosphate" }
                };
                SoilType[] soilTypes = new SoilType[]
                {
                    new() { Name = "Alfisols" },
                    new() { Name = "Aridisols" },
                    new() { Name = "Mollisols" },
                    new() { Name = "Ultisols" },
                    new() { Name = "Vertisols" },
                };
                StoreType[] storeTypes = new[]
                {
                    new StoreType() { Name = "Pharmacy" },
                    new StoreType() { Name = "Workers Center" },
                    new StoreType() { Name = "Agro T&M" }
                };
                EngineerType[] engineerTypes = new EngineerType[]
                {
                    new() { Name = "Admin" },
                    new() { Name = "Executive" }
                };
                List<Item> Items = new()
                {
                    new() { Name = "Orange", Price = 10000, ImagePath = "Orange.jpeg" },
                    new() { Name = "Apple", Price = 6000, ImagePath = "Apple.jpg" },
                    new() { Name = "Tomato", Price = 3500, ImagePath = "Tomato.jpg" },
                    new() { Name = "Potato", Price = 4000, ImagePath = "Potato.jpeg" },
                    new() { Name = "Grapes", Price = 8000, ImagePath = "Grapes.jpeg" }
                };
                List<LandType> landTypes = new();
                foreach (var item in Items)
                {
                    landTypes.Add(new() { Name = item.Name });
                }


                Buyer buyer = new()
                {
                    UserName = "buyer_Buyer",
                    Name = "Cobbie Peaddie",
                    Phone = "+963911111111"
                };
                List<Engineer> engineers = new()
                {
                    //Admin
                    new() { UserName = "admin_Admin", Name = "Jillie Corday", Phone = "+963911111111", Salary = 4100, EngineerType = engineerTypes[0] },

                    //Executives
                    new() { UserName = "executive_Executive", Name = "Launce Snepp", Phone = "+963911111111", Salary = 3700, EngineerType = engineerTypes[1] },
                    new() { UserName = "", Name = "Reta Petit", Phone = "+963911111111", Salary = 3200, EngineerType = engineerTypes[1] }
                };
                engineers[2].HeadEngineer = engineers[1];

                Farmer farmer = new()
                {
                    UserName = "farmer_Farmer",
                    Name = "Jack Smith",
                    Phone = "+963911111111"
                };
                List<Land> lands = new() {
                    new() { Farmer = farmer, Type = landTypes[0], SoilType = soilTypes[0], HasWell = true, Area = 10, Location = "Iowa" },
                    new() { Farmer = farmer, Type = landTypes[1], SoilType = soilTypes[1], HasWell = false, Area = 17, Location = "California" },
                    new() { Farmer = farmer, Type = landTypes[2], SoilType = soilTypes[2], HasWell = true, Area = 9, Location = "Kansas" },
                    new() { Farmer = farmer, Type = landTypes[3], SoilType = soilTypes[3], HasWell = false, Area = 11, Location = "Oklahoma" },
                    new() { Farmer = farmer, Type = landTypes[4], SoilType = soilTypes[4], HasWell = true, Area = 8, Location = "Texas" }
                };

                var stores = new List<Store>
                {
                    new Store { UserName = "store_Store", Type = storeTypes[0], Location = "LA, USA", Name = "Agro Pharmacy", ImagePath = "Agro Pharmacy.jpeg", Phone = "+963911111111" },
                    new Store { UserName = "store1_Store", Type = storeTypes[1], Location = "NY, USA", Name = "IAid", ImagePath = "IAid.jpeg", Phone = "+963911111111" },
                    new Store { UserName = "store2_Store", Type = storeTypes[2], Location = "TX, USA", Name = "Tool UP", ImagePath = "Tool UP.jpeg", Phone = "+963911111111" }
                };


                List<Order> orders = new()
                {
                    new() { Buyer = buyer, AdminEngineer = engineers[0], ExecutiveTeam = engineers[1], OrderDate = DateOnly.Parse("2023-04-04") },
                    new() { Buyer = buyer, AdminEngineer = engineers[0], ExecutiveTeam = engineers[1], OrderDate = DateOnly.Parse("2023-06-15") },
                    new() { Buyer = buyer, AdminEngineer = engineers[0], OrderDate = DateOnly.Parse("2023-08-01") },
                    new() { Buyer = buyer, AdminEngineer = engineers[0], OrderDate = DateOnly.FromDateTime(DateTime.Now) }
                };
                List<OrderDetail> orderDetails = new()
                {
                    new() { Order = orders[0], Item = Items[0], Kilos = 250, KiloPrice = Items[0].Price, DeliveryDate = DateOnly.Parse("2023-09-04") },
                    new() { Order = orders[1], Item = Items[1], Kilos = 500, KiloPrice = Items[1].Price, DeliveryDate = DateOnly.Parse("2024-01-04") },
                    new() { Order = orders[1], Item = Items[2], Kilos = 125, KiloPrice = Items[2].Price, DeliveryDate = DateOnly.Parse("2023-10-29") },
                    new() { Order = orders[2], Item = Items[3], Kilos = 400, KiloPrice = Items[3].Price, DeliveryDate = DateOnly.Parse("2024-04-11") },
                    new() { Order = orders[2], Item = Items[4], Kilos = 800, KiloPrice = Items[4].Price, DeliveryDate = DateOnly.Parse("2024-05-01") },
                    new() { Order = orders[3], Item = Items[2], Kilos = 800, KiloPrice = Items[2].Price, DeliveryDate = DateOnly.Parse("2024-03-04") }
                };
                List<Plan> plans = new() {
                    new() { Land = lands[0], OrderDetails = orderDetails[0], StartDate = DateOnly.Parse("2023-05-04"), FinishDate = DateOnly.Parse("2023-06-04"), Quantity = 74 },
                    new() { Land = lands[1], OrderDetails = orderDetails[1], StartDate = DateOnly.Parse("2023-06-04"), FinishDate = DateOnly.Parse("2023-06-04"), Quantity = 40 },
                    new() { Land = lands[2], OrderDetails = orderDetails[2], StartDate = DateOnly.Parse("2023-06-04"), FinishDate = DateOnly.Parse("2023-06-04"), Quantity = 43 },
                    new() { Land = lands[3], OrderDetails = orderDetails[3], StartDate = DateOnly.Parse("2023-06-04"), FinishDate = DateOnly.Parse("2023-06-04"), Quantity = 34 },
                    new() { Land = lands[4], OrderDetails = orderDetails[4], StartDate = DateOnly.Parse("2023-06-04"), FinishDate = DateOnly.Parse("2023-06-04"), Quantity = 13 }

                };
                List<Step> steps = new() {
                    new() { Plan = plans[0], Action = actions[0], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[0], Action = actions[1], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[1], Action = actions[2], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[1], Action = actions[3], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[2], Action = actions[0], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[2], Action = actions[1], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[3], Action = actions[2], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[3], Action = actions[3], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[4], Action = actions[4], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                    new() { Plan = plans[4], Action = actions[4], EstimatedFinishDate = DateOnly.Parse("2023-06-04"), EstimatedStartDate = DateOnly.Parse("2023-06-04"), IsChecked = false },
                };
                List<StepDetail> stepDetails = new() {
                    new() { Step = steps[0], AgriculturalItem = agriculturalItems[0], Quantity = 54 },
                    new() { Step = steps[1], AgriculturalItem = agriculturalItems[1], Quantity = 34 },
                    new() { Step = steps[2], AgriculturalItem = agriculturalItems[2], Quantity = 34 },
                    new() { Step = steps[3], AgriculturalItem = agriculturalItems[3], Quantity = 34 },
                    new() { Step = steps[4], AgriculturalItem = agriculturalItems[4], Quantity = 34 },
                    new() { Step = steps[5], AgriculturalItem = agriculturalItems[5], Quantity = 34 }
                };

                var purchases = new List<Purchase>
                {
                    new Purchase { Store = stores[0], Plan = plans[0], Date = DateOnly.Parse("2023-06-04") },
                    new Purchase { Store = stores[1], Plan = plans[1], Date = DateOnly.Parse("2023-06-04") },
                    new Purchase { Store = stores[2], Plan = plans[2], Date = DateOnly.Parse("2023-06-04") }
                };
                var purchaseDetails = new List<PurchaseDetail>
                {
                    new PurchaseDetail { Purchase = purchases[0], Item = "Tomato seeds", Quantity = 85, Price = 91, Details = "Integer aliquet." },
                    new PurchaseDetail { Purchase = purchases[0], Item = "Compound insecticides", Quantity = 69, Price = 492, Details = "Maecenas tincidunt lacus at velit." },
                    new PurchaseDetail { Purchase = purchases[1], Item = "Workers", Quantity = 5, Price = 260, IsRented = true },
                    new PurchaseDetail { Purchase = purchases[1], Item = "wheat seeds", Quantity = 67, Price = 412, Details = "Etiam pretium iaculis justo." },
                    new PurchaseDetail { Purchase = purchases[2], Item = "Potato seeds", Quantity = 602, Price = 71, Details = "Mauris sit amet eros." },
                    new PurchaseDetail { Purchase = purchases[2], Item = "Sctillage machine", Quantity = 18, Price = 184 }
                };



                if (!context.Actions.Any())
                {
                    await context.AddRangeAsync(actions);
                }

                if (!context.AgriculturalItems.Any())
                {
                    await context.AddRangeAsync(agriculturalItems);
                }

                if (!context.Buyers.Any())
                {
                    await context.AddAsync(buyer);
                }

                if (!context.Engineers.Any())
                {
                    await context.AddRangeAsync(engineers);
                }

                if (!context.EngineerTypes.Any())
                {
                    await context.AddRangeAsync(engineerTypes);
                }

                if (!context.Farmers.Any())
                {
                    await context.AddAsync(farmer);
                }

                if (!context.Lands.Any())
                {
                    await context.AddRangeAsync(lands);
                }

                if (!context.LandTypes.Any())
                {
                    await context.AddRangeAsync(landTypes);
                }

                if (!context.Items.Any())
                {
                    await context.AddRangeAsync(Items);
                }

                if (!context.Orders.Any())
                {
                    await context.AddRangeAsync(orders);
                }

                if (!context.OrderDetails.Any())
                {
                    await context.AddRangeAsync(orderDetails);
                }

                if (!context.Plans.Any())
                {
                    await context.AddRangeAsync(plans);
                }

                if (!context.Steps.Any())
                {
                    await context.AddRangeAsync(steps);
                }

                if (!context.StepDetails.Any())
                {
                    await context.AddRangeAsync(stepDetails);
                }

                if (!context.Purchases.Any())
                {
                    await context.AddRangeAsync(purchases);
                }

                if (!context.PurchaseDetails.Any())
                {
                    await context.AddRangeAsync(purchaseDetails);
                }

                if (!context.SoilTypes.Any())
                {
                    await context.AddRangeAsync(soilTypes);
                }

                if (!context.Stores.Any())
                {
                    await context.AddRangeAsync(stores);
                }

                if (!context.StoreTypes.Any())
                {
                    await context.AddRangeAsync(storeTypes);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}