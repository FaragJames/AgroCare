using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Newtonsoft.Json;
using Services;

namespace AgroCare.Controllers
{
    public class TestController : Controller
    {
        readonly PlanService planService;
        readonly PurchaseService purchaseService;
        readonly OrderService orderService;
        readonly LandService landService;
        readonly StepService stepService;
        readonly FarmerService farmerService;
        readonly StoreService storeService;
        readonly EngineerService engineerService;

        public TestController(PlanService planService, PurchaseService purchaseService,
            OrderService orderService, LandService landService, StepService stepService,
            FarmerService farmerService, StoreService storeService, EngineerService engineerService)
        {
            this.planService = planService;
            this.purchaseService = purchaseService;
            this.orderService = orderService;
            this.landService = landService;
            this.stepService = stepService;
            this.farmerService = farmerService;
            this.storeService = storeService;
            this.engineerService = engineerService;
        }
        public async Task<string> Index()
        {
            var purchases = purchaseService.GetAll();
            var farmer = await farmerService.GetAll().FirstAsync();
            var plans = planService.GetAllByFarmerId(farmer.Id);
            return JsonConvert.SerializeObject(plans, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }

        public async Task<bool> Delete()
        {
            var plan = await planService.GetOneAsync(5);
            if(plan != null)
                return await planService.RemoveAsync(plan);
            else
                return false;
        }
    }
}
