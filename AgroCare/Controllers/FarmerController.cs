using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        public IActionResult ShowPlans()
        {
            //Get then pass the farmer's id.
            return View();
        }

        [NonAction]
        public IActionResult ShowPurchases(long? id)
        {
            return View();
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
