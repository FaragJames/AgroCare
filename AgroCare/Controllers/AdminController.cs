using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Microsoft.AspNetCore.Authorization;
using Models.Models;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        public IActionResult ReceivedOrders()
        {
            //Get the admin's id.
            return View();
        }

        [NonAction]
        public IActionResult AccessDatabase()
        {
            //Blazor Database.
            return RedirectToPage("");
        }

        #region Add to an API Controller.
        [NonAction]
        public Order GetOrder(long? id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        [HttpPatch]
        public void PatchOrder(Order model)
        {

        }

        [NonAction]
        [HttpPost]
        public void FeedbackToBuyer(/*FeedbackViewModel model*/)
        {

        }
        #endregion
    }
}
