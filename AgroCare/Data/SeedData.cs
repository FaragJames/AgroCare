using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Newtonsoft.Json;

namespace AgroCare
{
    public class SeedData
    {
        static readonly string[] names = { "farmer", "buyer", "store", "admin", "executive" };
        static readonly string password = "Secert123$";



        public static async Task Seed(IApplicationBuilder app)
        {
            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var appIdentity = services.GetService<AppIdentityDbContext>();
            var context = services.GetService<AppDbContext>();
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            var userManager = services.GetService<UserManager<IdentityUser>>();

            string json, path = "Data/JsonDummyData/";

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

            if(context != null)
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
                engineers[0].EngineerType = engineerTypes[0];
                engineers[1].EngineerType = engineerTypes[0];
                engineers[2].EngineerType = engineerTypes[0];
                engineers[3].EngineerType = engineerTypes[1];
                engineers[4].EngineerType = engineerTypes[1];
                engineers[4].HeadEngineer = engineers[3];
                engineers[5].EngineerType = engineerTypes[1];
                engineers[5].HeadEngineer = engineers[3];
                engineers[6].EngineerType = engineerTypes[1];
                engineers[6].HeadEngineer = engineers[3];
                engineers[7].EngineerType = engineerTypes[1];
                engineers[8].EngineerType = engineerTypes[1];
                engineers[8].HeadEngineer = engineers[7];
                engineers[9].EngineerType = engineerTypes[1];
                engineers[9].HeadEngineer = engineers[7];

                List<Item> items = new();
                foreach (var item in landTypes)
                    items.Add(new Item() { Name = item.Name, Price = 24.4 });

                json = await File.ReadAllTextAsync($"{path}Farmer.json");
                Farmer[] farmers = JsonConvert.DeserializeObject<Farmer[]>(json)!;


                Order order = new()
                {
                    Buyer = buyers[0],
                    AdminEngineer = engineers[0],
                    ExecutiveTeam = engineers[3],
                    OrderDate = DateTime.Now
                };
                OrderDetail orderDetail = new()
                {
                    Order = order,
                    Item = items[0],
                    Kilos = 4,
                    KiloPrice = 123.23F,
                    DeliveryDate = DateTime.Now
                };
                Land land = new()
                {
                    Farmer = farmers[0],
                    Type = landTypes[0],
                    SoilType = soilTypes[0],
                    HasWell = true,
                    Area = 34.34f,
                    Location = "fasdf"
                };
                Plan plan = new()
                {
                    Land = land,
                    OrderDetails = orderDetail,
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                    Quantity = 34.43f
                };
                Step step = new()
                {
                    Plan = plan,
                    Action = actions[0],
                    EstimatedFinishDate = DateTime.Now,
                    EstimatedStartDate = DateTime.Now,
                    IsChecked = false
                };
                StepDetail stepDetail = new()
                {
                    Step = step,
                    AgriculturalItem = agriculturalItems[0],
                    Quantity = 34.4f
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
                    await context.AddAsync(land);
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
                    await context.AddAsync(order);
                }

                if (!context.OrderDetails.Any())
                {
                    await context.AddAsync(orderDetail);
                }

                if (!context.Plans.Any())
                {
                    await context.AddAsync(plan);
                }

                if (!context.Steps.Any())
                {
                    await context.AddAsync(step);
                }

                if (!context.StepDetails.Any())
                {
                    await context.AddAsync(stepDetail);
                }

                if (!context.Purchases.Any())
                {

                }

                if (!context.PurchaseDetails.Any())
                {

                }

                if (!context.SoilTypes.Any())
                {
                    await context.AddRangeAsync(soilTypes);
                }

                if (!context.Stores.Any())
                {

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
