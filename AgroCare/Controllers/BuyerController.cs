using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        [HttpGet]
        public IActionResult CreateOrder(long? id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel model)
        {
            //The model includes the buyer's id.

            //Algorithm (Queue Theory) for assigning an admin to the order.

            return View(nameof(ShowOrders));
        }

        [NonAction]
        [HttpPatch]
        public IActionResult CreateOrder()
        {
            return View(nameof(ShowOrders));
        }

        [NonAction]
        public IActionResult ShowOrders()
        {
            //Get then pass the buyer's id.
            throw new NotImplementedException();
        }

        #region Add to an API Controller.
        [NonAction]
        public IEnumerable<Order> GetPendingOrders(long? id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public IEnumerable<Order> GetUnderExecutionOrders(long? id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public IEnumerable<Order> GetExecutedOrders(long? id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}