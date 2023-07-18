using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;
using Services;

namespace AgroCare.Controllers
{
    public class TestController : Controller
    {
        IService<Plan> planService;
        private readonly IService<Purchase> purchaseService;
        private readonly IService<Order> orderService;
        private readonly IService<Land> landService;
        private readonly IService<Step> stepService;
        private readonly IService<Farmer> farmerService;
        private readonly IService<Store> storeService;
        private readonly IService<Engineer> engineerService;

        public TestController(IService<Plan> planService, IService<Purchase> purchaseService,
            IService<Order> orderService, IService<Land> landService, IService<Step> stepService,
            IService<Farmer> farmerService, IService<Store> storeService, IService<Engineer> engineerService)
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
            var plans = planService.GetAll();
            return JsonConvert.SerializeObject(plans, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }
    }
}
