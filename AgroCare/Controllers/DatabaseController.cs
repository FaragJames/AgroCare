using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services;
using System.Reflection;

namespace AgroCare.Controllers
{
    public class DatabaseController : Controller
    {
        readonly FarmerService farmerService;
        readonly LandService landService;
        readonly EngineerService engineerService;
        readonly IService<Buyer> buyerService;
        readonly OrderService orderService;
        readonly IService<OrderDetail> orderDetailService;
        readonly IService<Item> itemService;
        readonly PlanService planService;
        readonly StepService stepService;
        readonly IService<StepDetail> stepDetailService;
        readonly StoreService storeService;
        readonly PurchaseService purchaseService;
        readonly IService<PurchaseDetail> purchaseDetailService;

        Dictionary<string, object> servicesDictionary = new();
        readonly IMapper mapper;


        public DatabaseController(FarmerService farmerService, LandService landService,
            EngineerService engineerService, IService<Buyer> buyerService,
            OrderService orderService, IService<OrderDetail> orderDetailService,
            IService<Item> itemService, PlanService planService, StepService stepService,
            IService<StepDetail> stepDetailService, StoreService storeService,
            PurchaseService purchaseService, IService<PurchaseDetail> purchaseDetailService,
            IMapper mapper)
        {
            this.farmerService = farmerService;
            servicesDictionary.Add(farmerService.GetType().FullName!, farmerService);

            this.landService = landService;
            servicesDictionary.Add(landService.GetType().FullName!, landService);

            this.engineerService = engineerService;
            servicesDictionary.Add(engineerService.GetType().FullName!, engineerService);

            this.buyerService = buyerService;
            servicesDictionary.Add(buyerService.GetType().FullName!, buyerService);

            this.orderService = orderService;
            servicesDictionary.Add(orderService.GetType().FullName!, orderService);

            this.orderDetailService = orderDetailService;
            servicesDictionary.Add(orderDetailService.GetType().FullName!, orderDetailService);

            this.itemService = itemService;
            servicesDictionary.Add(itemService.GetType().FullName!, itemService);

            this.planService = planService;
            servicesDictionary.Add(planService.GetType().FullName!, planService);

            this.stepService = stepService;
            servicesDictionary.Add(stepService.GetType().FullName!, stepService);

            this.stepDetailService = stepDetailService;
            servicesDictionary.Add(stepDetailService.GetType().FullName!, stepDetailService);

            this.storeService = storeService;
            servicesDictionary.Add(storeService.GetType().FullName!, storeService);

            this.purchaseService = purchaseService;
            servicesDictionary.Add(purchaseService.GetType().FullName!, purchaseService);

            this.purchaseDetailService = purchaseDetailService;
            servicesDictionary.Add(purchaseDetailService.GetType().FullName!, purchaseDetailService);
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, Executive")]
        public IActionResult ViewFarmers()
        {
            return View(farmerService.GetAll().Select(entity => mapper.Map<FarmerDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin, Executive")]
        public IActionResult ViewLands()
        {
            return View(landService.GetAll().Select(entity => mapper.Map<LandDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewEngineers()
        {
            return View(engineerService.GetAll().Select(entity => mapper.Map<EngineerDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewBuyers()
        {
            return View(buyerService.GetAll().Select(entity => mapper.Map<BuyerDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewOrders()
        {
            return View(orderService.GetAll().Select(entity => mapper.Map<OrderDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewOrderDetails()
        {
            return View(orderService.GetAll().Select(entity => mapper.Map<OrderDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewItems()
        {
            return View(itemService.GetAll().Select(entity => mapper.Map<ItemDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin, Executive")]
        public IActionResult ViewPlans()
        {
            return View(planService.GetAll().Select(entity => mapper.Map<PlanDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin, Executive")]
        public IActionResult ViewSteps()
        {
            return View(planService.GetAll().Select(entity => mapper.Map<PlanDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin, Executive")]
        public IActionResult ViewStepDetails()
        {
            return View(stepService.GetAll().Select(entity => mapper.Map<StepDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewStores()
        {
            return View(storeService.GetAll().Select(entity => mapper.Map<StoreDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewPurchases()
        {
            return View(purchaseService.GetAll().Select(entity => mapper.Map<PurchaseDto>(entity)).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewPurchaseDetails()
        {
            return View(purchaseService.GetAll().Select(entity => mapper.Map<PurchaseDto>(entity)).ToList());
        }


        [Authorize(Roles = "Admin, Executive")]
        [HttpPost]
        public string Add([FromBody] dynamic body)
        {
            try
            {
                object entity = GetObject(body);
                string objectName = body.key;


                (string serviceName, object service) = servicesDictionary.Where(entry => entry.Key.Contains(objectName))
                    .First();
                Type serviceType = Type.GetType(serviceName + ", Services")!,
                    objectType = Type.GetType("Models.Models." + objectName + ", Models")!;


                MethodInfo method = serviceType.GetMethod("AddAsync", new Type[] { objectType })!;
                method.Invoke(service, new object[] { entity });

                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        [Authorize(Roles = "Admin, Executive")]
        [HttpPost]
        public string Edit([FromBody] dynamic body, [FromServices] AppDbContext context)
        {
            try
            {
                object entity = GetObject(body);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();

                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        [Authorize(Roles = "Admin, Executive")]
        [HttpPost]
        public string Delete([FromBody] dynamic body, [FromServices] AppDbContext context)
        {
            try
            {
                object entity = GetObject(body);
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();

                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        private Object GetObject(dynamic body)
        {
            string key = (string)body.key;
            object toReturn = new();
            switch (key)
            {
                case nameof(Buyer):
                    {
                        toReturn = new Buyer()
                        {
                            Id = (int)body.Id,
                            Name = (string)body.Name,
                            Phone = (string)body.Phone,
                            UserName = (string)body.UserName
                        };
                        break;
                    }

                case nameof(Engineer):
                    {
                        var engineer = new Engineer()
                        {
                            Id = (int)body.Id,
                            Name = (string)body.Name,
                            Phone = (string)body.Phone,
                            UserName = (string)body.UserName,
                            Salary = (float)body.Salary,
                            EngineerTypeId = (int)body.EngineerTypeId,
                        };
                        try
                        {
                            int tmp = (int)body.HeadEngineerId;
                            engineer.HeadEngineerId = tmp;
                        }
                        catch (Exception)
                        {
                        }
                        toReturn = engineer;
                        break;
                    }

                case nameof(Farmer):
                    {
                        toReturn = new Farmer()
                        {
                            Id = (int)body.Id,
                            Name = (string)body.Name,
                            Phone = (string)body.Phone,
                            UserName = (string)body.UserName
                        };
                        break;
                    }

                case nameof(Item):
                    {
                        toReturn = new Item()
                        {
                            Id = (int)body.Id,
                            Name = (string)body.Name,
                            Price = (float)body.Price,
                            ImagePath = ((string)body.ImagePath).Split(new char[] { '/', '\\' })[^1]
                        };
                        break;
                    }

                case nameof(Land):
                    {
                        toReturn = new Land()
                        {
                            Id = (int)body.Id,
                            Location = (string)body.Location,
                            Area = (float)body.Area,
                            FarmerId = (int)body.FarmerId,
                            HasWell = (bool)body.HasWell,
                            SoilTypeId = (int)body.SoilTypeId,
                            TypeId = (int)body.TypeId
                        };
                        break;
                    }

                case nameof(Order):
                    {
                        var order = new Order()
                        {
                            Id = (int)body.Id,
                            AdminEngineerId = (int)body.AdminEngineerId,
                            BuyerId = (int)body.BuyerId,
                            OrderDate = (DateOnly)body.OrderDate
                        };
                        try
                        {
                            int tmp = (int)body.ExecutiveTeamId;
                            order.ExecutiveTeamId = tmp;
                        }
                        catch (Exception)
                        {
                        }
                        toReturn = order;
                        break;
                    }

                case nameof(OrderDetail):
                    {
                        var orderDetail = new OrderDetail()
                        {
                            Id = (int)body.Id,
                            DeliveryDate = (DateOnly)body.DeliveryDate,
                            ItemId = (int)body.ItemId,
                            KiloPrice = (float)body.KiloPrice,
                            Kilos = (int)body.Kilos,
                            OrderId = (int)body.OrderId
                        };
                        try
                        {
                            int tmp = (int)body.Feedback;
                        }
                        catch (Exception)
                        {
                            string tmp = (string)body.Feedback;
                            orderDetail.Feedback = tmp;
                        }
                        toReturn = orderDetail;
                        break;
                    }

                case nameof(Plan):
                    {
                        toReturn = new Plan()
                        {
                            Id = (int)body.Id,
                            FinishDate = (DateOnly)body.FinishDate,
                            LandId = (int)body.LandId,
                            OrderDetailsId = (int)body.OrderDetailsId,
                            Quantity = (float)body.Quantity,
                            StartDate = (DateOnly)body.StartDate
                        };
                        break;
                    }

                case nameof(Purchase):
                    {
                        toReturn = new Purchase()
                        {
                            Id = (int)body.Id,
                            Date = (DateOnly)body.Date,
                            PlanId = (int)body.PlanId,
                            StoreId = (int)body.StoreId
                        };
                        break;
                    }

                case nameof(PurchaseDetail):
                    {
                        var purchase = new PurchaseDetail()
                        {
                            Id = (int)body.Id,
                            IsRented = (bool)body.IsRented,
                            PurchaseId = (int)body.PurchaseId,
                            Price = (float)body.Price,
                            Quantity = (int)body.Quantity,
                            Item = (string)body.Item
                        };
                        try
                        {
                            int tmp = (int)body.Details;
                        }
                        catch (Exception)
                        {
                            string tmp = (string)body.Details;
                            purchase.Details = tmp;
                        }
                        toReturn = purchase;
                        break;
                    }

                case nameof(Step):
                    {
                        toReturn = new Step()
                        {
                            Id = (int)body.Id,
                            ActionId = (int)body.ActionId,
                            EstimatedFinishDate = (DateOnly)body.EstimatedFinishDate,
                            EstimatedStartDate = (DateOnly)body.EstimatedStartDate,
                            IsChecked = (bool)body.IsChecked,
                            PlanId = (int)body.PlanId
                        };
                        break;
                    }

                case nameof(StepDetail):
                    {
                        toReturn = new StepDetail()
                        {
                            Id = (int)body.Id,
                            StepId = (int)body.StepId,
                            AgriculturalItemId = (int)body.AgriculturalItemId,
                            Quantity = (float)body.Quantity
                        };
                        break;
                    }

                case nameof(Store):
                    {
                        toReturn = new Store()
                        {
                            Id = (int)body.Id,
                            ImagePath = ((string)body.ImagePath).Split(new char[] { '/', '\\' })[^1],
                            Location = (string)body.Location,
                            Name = (string)body.Name,
                            Phone = (string)body.Phone,
                            TypeId = (int)body.TypeId,
                            UserName = (string)body.UserName
                        };
                        break;
                    }
            }

            return toReturn;
        }
    }
}
