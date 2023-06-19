using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class Farmer : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
