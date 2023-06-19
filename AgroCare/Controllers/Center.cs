using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    public class Center : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
