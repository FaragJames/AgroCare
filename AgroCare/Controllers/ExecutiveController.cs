using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Executive")]
    public class ExecutiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        public IActionResult ReceivedTasks()
        {
            //Get then pass the executive's id.
            return View();
        }

        [NonAction]
        [HttpGet]
        public IActionResult CreatePlan(long? id)
        {
            return View();
        }

        [NonAction]
        [HttpPost]
        public IActionResult CreatePlan(PlanViewModel model)
        {
            //The model includes the executive's id.
            return View(nameof(ReceivedTasks));
        }

        [NonAction]
        public IActionResult AccessDatabase()
        {
            //Blazor Database.
            return RedirectToPage("");
        }

        #region Add to an API Controller.
        [NonAction]
        public Order GetTask(long? id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
