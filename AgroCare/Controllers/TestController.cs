using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services;

namespace AgroCare.Controllers
{
    public class TestController : Controller
    {
        IService<Plan> planService;



        public TestController(IService<Plan> service) {
            planService = service;
        }
        public async Task<string> Index()
        {
            var entity = planService.GetAll().FirstOrDefault();
            return (await planService.RemoveAsync(entity ?? new Plan())).ToString();
        }

        public async Task<string> GetOne()
        {
            var p = await planService.GetOneAsync(1);
            return p == null ? "null" : "not null";
        }
    }
}
