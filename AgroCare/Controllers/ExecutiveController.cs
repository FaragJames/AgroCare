using AgroCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Executive")]
    public class ExecutiveController : Controller
    {
        OrderService orderservice;
        LandService landservice;
        PlanService planservice;
        IService<AgriculturalItem> agriItemService;
        IService<Item> Itemservices;
        IService<Models.Models.Action> actionservice;


        public ExecutiveController(OrderService orderService, IService<Item> itemservices, IService<Models.Models.Action> actionservice, LandService landservice, PlanService planservice, IService<AgriculturalItem> agriItemService)
        {
            this.orderservice = orderService;
            Itemservices = itemservices;
            this.actionservice = actionservice;
            this.landservice = landservice;
            this.planservice = planservice;
            this.agriItemService = agriItemService;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePlan(int orderId = 0)
        {
            if(orderId == 0)
            {
                return RedirectToAction(nameof(DatabaseController.ViewPlans), "Database");
            }
            Plsn_Step_Oreder_DetailsView plsn_Step_Oreder_DetailsView = new Plsn_Step_Oreder_DetailsView()
            {
                Actions = await actionservice.GetAll().ToListAsync(),
                AgriculturalItems = await agriItemService.GetAll().ToListAsync(),
                orderdetails = (await orderservice.GetOneAsync(orderId))!.OrderDetails.ToList()
            };
            return View(plsn_Step_Oreder_DetailsView);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(PlanViewModel planViewModel)
        {
            if (await landservice.DoesExist(planViewModel.LandId))
            {
                List<Step> steps = new List<Step>();
                List<StepDetail> stepsdetails = new List<StepDetail>();

                foreach (var PSSD in planViewModel.planStepDetailsView)
                {
                    stepsdetails.Add(new StepDetail { AgriculturalItemId = PSSD.AgriculturalItemId, Quantity = PSSD.Quantity1 });
                    steps.Add(new Step { StepDetails = stepsdetails, ActionId = PSSD.ActionId, EstimatedStartDate = PSSD.EstimatedStartDate, EstimatedFinishDate = PSSD.EstimatedFinishDate });
                    stepsdetails = new List<StepDetail>();
                }
                Plan plan = new Plan()
                {
                    LandId = planViewModel.LandId,
                    StartDate = planViewModel.StartDate,
                    Quantity = planViewModel.Quantity,
                    OrderDetailsId = planViewModel.OrderDetailId,
                    Steps = steps
                };
                if (!await planservice.AddAsync(plan))
                {
                    return BadRequest(new { error = "Not Added Try Again..!" });
                }
                else
                    return Ok();
            }
            else
                return BadRequest(new { error = "There is no such this Land ID.." });

        }
    }
}
