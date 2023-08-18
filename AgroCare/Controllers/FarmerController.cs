using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models.Models;
using Newtonsoft.Json.Linq;
using Services;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly PlanService planService;
        private readonly PurchaseService purchaseService;
        private readonly UserIdService<Farmer> userInfo;
        private readonly IMapper mapper;


        public FarmerController(PurchaseService purchaseService, PlanService planService,
            UserIdService<Farmer> userInfo, IMapper mapper)
        {
            this.planService = planService;
            this.purchaseService = purchaseService;
            this.userInfo = userInfo;
            this.mapper = mapper;
        }

        public async Task<IActionResult> ShowPlans()
        {
            var farmerId = await userInfo.GetIdByUserNameAsync(User.Identity!.Name!);
            var farmerPlans = planService.GetAllByFarmerId(farmerId)
                .Select(i => mapper.Map<PlanDto>(i)).ToList();

            ViewBag.farmerId = farmerId;
            return View(farmerPlans);
        }

        public async Task<IActionResult> ShowPurchases()
        {
            var farmerId = await userInfo.GetIdByUserNameAsync(User.Identity!.Name!);
            var farmerPlansIds = planService.GetAllByFarmerId(farmerId).Select(p => p.Id).ToList();
            List<PurchaseDto> purchasesDto = new();

            foreach (var planId in farmerPlansIds)
                purchasesDto.AddRange(purchaseService.GetAll()
                    .Where(p => p.PlanId == planId).Select(i => mapper.Map<PurchaseDto>(i)).ToList());

            return View(purchasesDto);
        }

        [HttpPost]
        public async Task CheckStep([FromServices] StepService stepService, [FromBody] dynamic body)
        {
            int stepId = (int)body.stepId;
            Step step = (await stepService.GetOneAsync(stepId))!;
            step.IsChecked = true;
            await stepService.EditAsync(step);
        }

        [HttpPost]
        public string GenerateCode([FromBody] dynamic body)
        {
            int farmerId = (int) body.farmerId,
                planId = (int)body.planId;
            string code = Guid.NewGuid().ToString()[..8];
            Program.PlansCodes[farmerId] = (code, planId);
            return code;
        }

        #region Add to an API Controller.
        [NonAction]
        public Plan GetPlan(long? id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        [HttpPost]
        public string GeneratePlanCode(long? id)
        {
            //'id' is the plan's id.
            throw new NotImplementedException();
        }

        [NonAction]
        [HttpPost]
        public bool CheckStep(long? id)
        {
            //'id' is the step's id.
            throw new NotImplementedException();
        }
        #endregion
    }
}
