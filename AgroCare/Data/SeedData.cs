using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Newtonsoft.Json;

namespace AgroCare.Data
{
    public class SeedData
    {
        static readonly string[] names = { "farmer", "buyer", "store", "admin", "executive" };
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
                    foreach (var role in names)
                    {
                        await roleManager.CreateAsync(new($"{char.ToUpper(role[0]) + role[1..]}"));

                    }
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

            string json, path = "Data/JsonDummyData/";
            if (context != null)
            {
                await context.Database.MigrateAsync();


                Models.Models.Action[] actions = new Models.Models.Action[]
                {
                    new() { Name = "Plow" },
                    new() { Name = "Level" },
                    new() { Name = "Test Soil" },
                    new() { Name = "Fertilize" },
                    new() { Name = "Seed" }
                };
                AgriculturalItem[] agriculturalItems = new AgriculturalItem[]
                {
                    new() { Name = "Axe" },
                    new() { Name = "Rake" },
                    new() { Name = "Shovel" },
                    new() { Name = "Gardening Fork" },
                    new() { Name = "Wheel Barrow" },
                    new() { Name = "Tractor" },
                };
                LandType[] landTypes = new LandType[]
                {
                    new() { Name = "Orange" },
                    new() { Name = "Apple" },
                    new() { Name = "Tomato" },
                    new() { Name = "Potato" },
                    new() { Name = "Grape" }
                };
                SoilType[] soilTypes = new SoilType[]
                {
                    new() { Name = "Sandy" },
                    new() { Name = "Inceptisol" },
                    new() { Name = "Peat" },
                    new() { Name = "Clay" },
                    new() { Name = "Aridisol" },
                    new() { Name = "Chernozem" }
                };
                StoreType[] storeTypes = new[]
                {
                    new StoreType() { Name = "Pharmacy" },
                    new StoreType() { Name = "Workers Center" },
                    new StoreType() { Name = "Shop" }
                };
                EngineerType[] engineerTypes = new EngineerType[]
                {
                    new() { Name = "Admin" },
                    new() { Name = "Executive" }
                };


                json = await File.ReadAllTextAsync($"{path}Buyer.json");
                Buyer[] buyers = JsonConvert.DeserializeObject<Buyer[]>(json)!;

                json = await File.ReadAllTextAsync($"{path}Engineer.json");
                Engineer[] engineers = JsonConvert.DeserializeObject<Engineer[]>(json)!;
                //Admins.
                engineers[0].EngineerType = engineerTypes[0];
                engineers[1].EngineerType = engineerTypes[0];
                engineers[2].EngineerType = engineerTypes[0];
                //Head Executives.
                engineers[3].EngineerType = engineerTypes[1];
                engineers[7].EngineerType = engineerTypes[1];
                //Executives.
                engineers[4].EngineerType = engineerTypes[1];
                engineers[4].HeadEngineer = engineers[3];
                engineers[5].EngineerType = engineerTypes[1];
                engineers[5].HeadEngineer = engineers[3];
                engineers[6].EngineerType = engineerTypes[1];
                engineers[6].HeadEngineer = engineers[3];
                engineers[8].EngineerType = engineerTypes[1];
                engineers[8].HeadEngineer = engineers[7];
                engineers[9].EngineerType = engineerTypes[1];
                engineers[9].HeadEngineer = engineers[7];

                List<Item> items = new();
                foreach (var item in landTypes)
                    items.Add(new Item() { Name = item.Name, Price = 24.4 });

                json = await File.ReadAllTextAsync($"{path}Farmer.json");
                Farmer[] farmers = JsonConvert.DeserializeObject<Farmer[]>(json)!;


                List<Order> orders = new()
                {
                    new() { Buyer = buyers[0], AdminEngineer = engineers[0], ExecutiveTeam = engineers[3], OrderDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Buyer = buyers[1], AdminEngineer = engineers[1], ExecutiveTeam = engineers[3], OrderDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Buyer = buyers[2], AdminEngineer = engineers[2], ExecutiveTeam = engineers[7], OrderDate = DateOnly.FromDateTime(DateTime.Now) }
                };
                List<OrderDetail> orderDetails = new()
                {
                    new() { Order = orders[0], Item = items[0], Kilos = 4, KiloPrice = 123.23F, DeliveryDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Order = orders[1], Item = items[1], Kilos = 4, KiloPrice = 123.23F, DeliveryDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Order = orders[1], Item = items[2], Kilos = 4, KiloPrice = 123.23F, DeliveryDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Order = orders[2], Item = items[3], Kilos = 4, KiloPrice = 123.23F, DeliveryDate = DateOnly.FromDateTime(DateTime.Now) },
                    new() { Order = orders[2], Item = items[4], Kilos = 4, KiloPrice = 123.23F, DeliveryDate = DateOnly.FromDateTime(DateTime.Now) }
                };
                List<Land> lands = new() {
                    new() { Farmer = farmers[0], Type = landTypes[0], SoilType = soilTypes[0], HasWell = true, Area = 34.34f, Location = "fasdf" },
                    new() { Farmer = farmers[1], Type = landTypes[1], SoilType = soilTypes[1], HasWell = true, Area = 34.34f, Location = "fasdf" },
                    new() { Farmer = farmers[2], Type = landTypes[2], SoilType = soilTypes[2], HasWell = true, Area = 34.34f, Location = "fasdf" },
                    new() { Farmer = farmers[1], Type = landTypes[3], SoilType = soilTypes[3], HasWell = true, Area = 34.34f, Location = "fasdf" },
                    new() { Farmer = farmers[0], Type = landTypes[4], SoilType = soilTypes[4], HasWell = true, Area = 34.34f, Location = "fasdf" }
                };
                List<Plan> plans = new() {
                    new() { Land = lands[0], OrderDetails = orderDetails[0], StartDate = DateOnly.FromDateTime(DateTime.Now), FinishDate = DateOnly.FromDateTime(DateTime.Now), Quantity = 34.43f },
                    new() { Land = lands[1], OrderDetails = orderDetails[1], StartDate = DateOnly.FromDateTime(DateTime.Now), FinishDate = DateOnly.FromDateTime(DateTime.Now), Quantity = 34.43f },
                    new() { Land = lands[2], OrderDetails = orderDetails[2], StartDate = DateOnly.FromDateTime(DateTime.Now), FinishDate = DateOnly.FromDateTime(DateTime.Now), Quantity = 34.43f },
                    new() { Land = lands[3], OrderDetails = orderDetails[3], StartDate = DateOnly.FromDateTime(DateTime.Now), FinishDate = DateOnly.FromDateTime(DateTime.Now), Quantity = 34.43f },
                    new() { Land = lands[4], OrderDetails = orderDetails[4], StartDate = DateOnly.FromDateTime(DateTime.Now), FinishDate = DateOnly.FromDateTime(DateTime.Now), Quantity = 34.43f }

                };
                List<Step> steps = new() {
                    new() { Plan = plans[0], Action = actions[0], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[0], Action = actions[1], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[1], Action = actions[2], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[1], Action = actions[3], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[2], Action = actions[0], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[2], Action = actions[1], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[3], Action = actions[2], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[3], Action = actions[4], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[4], Action = actions[3], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                    new() { Plan = plans[4], Action = actions[4], EstimatedFinishDate = DateOnly.FromDateTime(DateTime.Now), EstimatedStartDate = DateOnly.FromDateTime(DateTime.Now), IsChecked = false },
                };
                List<StepDetail> stepDetails = new() {
                    new() { Step = steps[0], AgriculturalItem = agriculturalItems[0], Quantity = 34.4f },
                    new() { Step = steps[1], AgriculturalItem = agriculturalItems[1], Quantity = 34.4f },
                    new() { Step = steps[2], AgriculturalItem = agriculturalItems[2], Quantity = 34.4f },
                    new() { Step = steps[3], AgriculturalItem = agriculturalItems[3], Quantity = 34.4f },
                    new() { Step = steps[4], AgriculturalItem = agriculturalItems[4], Quantity = 34.4f },
                    new() { Step = steps[5], AgriculturalItem = agriculturalItems[5], Quantity = 34.4f }
                };

                var stores = new List<Store>
                {
                    new Store { Type = storeTypes[0], Location = "Sheridan", Name = "Klocko, Bailey and Mayer" },
                    new Store { Type = storeTypes[2], Location = "Lakewood", Name = "Robel-Kozey" },
                    new Store { Type = storeTypes[1], Location = "Scott", Name = "Schumm, Bruen and Bednar" }
                };
                var purchases = new List<Purchase>
                {
                    new Purchase { Store = stores[0], Plan = plans[0], Date = DateOnly.FromDateTime(DateTime.Now) },
                    new Purchase { Store = stores[2], Plan = plans[1], Date = DateOnly.FromDateTime(DateTime.Now) },
                    new Purchase { Store = stores[1], Plan = plans[2], Date = DateOnly.FromDateTime(DateTime.Now) }
                };
                var purchaseDetails = new List<PurchaseDetail>
                {
                    new PurchaseDetail { Purchase = purchases[0], Item = "Tomato seeds", Quantity = 85, Price = 91.95F, Details = "Integer aliquet." },
                    new PurchaseDetail { Purchase = purchases[0], Item = "Compound insecticides", Quantity = 69, Price = 492.91F, Details = "Maecenas tincidunt lacus at velit." },
                    new PurchaseDetail { Purchase = purchases[1], Item = "Natural fertilizer", Quantity = 70, Price = 262.07F, Details = "Lorem ipsum dolor sit amet." },
                    new PurchaseDetail { Purchase = purchases[1], Item = "wheat seeds", Quantity = 67, Price = 412.77F, Details = "Etiam pretium iaculis justo." },
                    new PurchaseDetail { Purchase = purchases[2], Item = "Potato seeds", Quantity = 602, Price = 71.17F, Details = "Mauris sit amet eros." },
                    new PurchaseDetail { Purchase = purchases[2], Item = "Sctillage machine", Quantity = 18, Price = 184.09F, Details = "Donec ut dolor." }
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
                    await context.AddRangeAsync(buyers);
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
                    await context.AddRangeAsync(farmers);
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
                    await context.AddRangeAsync(items);
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
