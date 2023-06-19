using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class Buyer : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
