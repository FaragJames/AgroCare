using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Newtonsoft.Json;
using Services;
using System.Numerics;

namespace AgroCare.Controllers
{
    public class TestController : Controller
    {
        private readonly IService<Models.Models.Action> actionService;
        private readonly IMapper mapper;
        private readonly IService<AgriculturalItem> agriItemService;
        private readonly IService<OrderDetail> orderDetailService;
        private readonly IService<Item> ItemService;
        private readonly IService<Engineer> engineerService;
        private readonly IService<Buyer> buyerService;
        readonly PlanService planService;
        readonly OrderService orderService;
        private readonly PurchaseService purchaseService;
        readonly LandService landService;
        readonly FarmerService farmerService;

        public TestController(IService<Models.Models.Action> actionService, IMapper mapper,
            IService<AgriculturalItem> agriItemService, IService<OrderDetail> orderDetailService,
            IService<Item> ItemService, IService<Engineer> engineerService, IService<Buyer> buyerService,
            PlanService planService,OrderService orderService, PurchaseService purchaseService,
            LandService landService, FarmerService farmerService)
        {
            this.actionService = actionService;
            this.mapper = mapper;
            this.agriItemService = agriItemService;
            this.orderDetailService = orderDetailService;
            this.ItemService = ItemService;
            this.engineerService = engineerService;
            this.buyerService = buyerService;
            this.planService = planService;
            this.orderService = orderService;
            this.purchaseService = purchaseService;
            this.landService = landService;
            this.farmerService = farmerService;
        }

        public async Task<string> Index()
        {
            await Task.Delay(1);
            var orders = planService.GetAll().ToList();
            return ToJson<Plan, PlanDto>(orders);
            //return JsonConvert.SerializeObject(orders, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        public async Task<string> Edit(int? id)
        {
            var order = orderService.GetAll().First();
            if (id.HasValue)
                order = await orderService.GetOneAsync(id.Value);

            if(order != null)
            {
                order.OrderDate = DateOnly.MaxValue;
                order.OrderDetails.Add(new OrderDetail
                {
                    DeliveryDate = DateOnly.MinValue.AddDays(1),
                    Kilos = 33,
                    KiloPrice = 23.234F,
                    Item = ItemService.GetAll().First()
                });
            }

            return await orderService.EditAsync(order!) ?
                ToJson<Order, OrderDto>(order!) : false.ToString();
        }
        public async Task<string> Add()
        {
            Order order = new()
            {
                AdminEngineer = engineerService.GetAll().First(e => e.EngineerType.Name.Contains("admin")),
                Buyer = buyerService.GetAll().First(),
                OrderDate = DateOnly.MinValue,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail
                    {
                        DeliveryDate = DateOnly.MaxValue,
                        Kilos = 33,
                        KiloPrice = 23.234F,
                        Item = ItemService.GetAll().First()
                    }
                }
            };
            return await orderService.AddAsync(order) ?
                ToJson<Order, OrderDto>(order) : false.ToString();
        }
        public async Task<bool> Delete(int? id)
        {
            var order = orderService.GetAll().First();
            if (id.HasValue)
                order = await orderService.GetOneAsync(id.Value);

            if (order != null)
                return await orderService.RemoveAsync(order);
            else
                return false;
        }
        public string Testing()
        {
            var result = purchaseService.GetPendingPurchases().ToList();
            return ToJson<Order, OrderDto>(result);
            //return ToJson<Order, OrderDto>(orderService.GetExecutiveTeamOrders(engineerService.GetAll()
            //    .First(e => e.EngineerType.Name.Contains("executive") && !e.HeadEngineerId.HasValue).Id));
        }

        private string ToJson<TSource, TDestination>(object entity)
        {
            if (entity is IEnumerable<TSource> source)
                entity = source.Select(element => mapper.Map<TDestination>(element)).ToList();
            else
                entity = mapper.Map<TDestination>(entity) ?? entity;
            return JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        #region Testing PlanService
        //public async Task<string> Index()
        //{
        //    await Task.Delay(1);
        //    var plans = planService.GetAll().AsEnumerable();
        //    return ToJson<Plan, PlanDto>(plans);
        //}
        //public async Task<string> Edit(int? id)
        //{
        //    var plan = planService.GetAll().First();
        //    if(id.HasValue)
        //        plan = await planService.GetOneAsync(id.Value);

        //    plan!.Quantity = 100000.4843f;
        //    plan.Steps.Add(new Step
        //    {
        //        ActionId = actionService.GetAll().First().Id,
        //        IsChecked = true,
        //        EstimatedFinishDate = DateOnly.MaxValue,
        //        EstimatedStartDate = DateOnly.MinValue,
        //        StepDetails = new List<StepDetail>
        //        {
        //            new StepDetail {
        //                AgriculturalItemId = itemService.GetAll().First().Id,
        //                Quantity = 342.4f
        //            }
        //        }
        //    });

        //    return await planService.EditAsync(plan) ?
        //        ToJson<Plan, PlanDto>(plan) : false.ToString();
        //}
        //public async Task<string> Add()
        //{
        //    Plan plan = new()
        //    {
        //        OrderDetailsId = orderDetailService.GetAll().First().Id,
        //        FinishDate = DateOnly.MaxValue,
        //        StartDate = DateOnly.MinValue,
        //        Land = landService.GetAll().First(),
        //        Steps = new List<Step>
        //        {
        //            new Step
        //            {
        //                Action = actionService.GetAll().First(),
        //                IsChecked = true,
        //                EstimatedFinishDate = DateOnly.MaxValue,
        //                EstimatedStartDate = DateOnly.MinValue,
        //                StepDetails = new List<StepDetail>
        //                {
        //                    new StepDetail {
        //                        AgriculturalItemId = itemService.GetAll().First().Id,
        //                        Quantity = 342.4f
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    return await planService.AddAsync(plan) ?
        //        ToJson<Plan, PlanDto>(plan) : false.ToString();
        //}
        //public async Task<bool> Delete(int? id)
        //{
        //    var plan = planService.GetAll().First();
        //    if(id.HasValue)
        //        plan = await planService.GetOneAsync(id.Value);

        //    if(plan != null)
        //        return await planService.RemoveStepsAsync(plan);
        //    else
        //        return false;
        //}
        #endregion
    }
}
