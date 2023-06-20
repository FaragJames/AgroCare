using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Store")]
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        [HttpGet]
        public IActionResult CreatePurchase(long? id)
        {
            return View();
        }

        [NonAction]
        [HttpPost]
        public IActionResult CreatePurchase(/*PurchaseViewModel model*/)
        {
            //The model includes the center's id.
            return View(nameof(ShowPurchases));
        }

        [NonAction]
        public IActionResult ShowPurchases()
        {
            //Get then pass the center's id.
            return View();
        }

        #region Add to an API Controller.
        [NonAction]
        public IEnumerable<Purchase> GetPaidPurchases(long? id)
        {
            throw new NotImplementedException();
        }

        //Add to an API Controller.
        [NonAction]
        public IEnumerable<Purchase> GetUnPaidPurchases(long? id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
